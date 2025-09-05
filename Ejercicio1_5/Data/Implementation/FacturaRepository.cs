using Ejercicio1_5.Data.Helper;
using Ejercicio1_5.Data.Interface;
using Ejercicio1_5.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ejercicio1_5.Data.Implementation
{
    //Lo intenté pero el getall y el getbyid no me sale :(
    public class FacturaRepository : IFacturaRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;
        public FacturaRepository()
        {
            _connection = DataHelper.GetInstance().GetConnection();
            _transaction = null!;
        }
        
        public List<Factura> GetAll()
        {
            var facturas = new List<Factura>();

            try
            {
                var dt = DataHelper.GetInstance().ExecuteSP("SP_CONSULTAR_FACTURAS", null);

                Factura? factura = null;
                int facturaIdActual = -1;

                foreach (DataRow row in dt.Rows)
                {
                    int idFactura = Convert.ToInt32(row["id_factura"]);

                    if (idFactura != facturaIdActual)
                    {
                        factura = new Factura
                        {
                            Id = idFactura,
                            Cliente = row["cliente"].ToString()!,
                            Vendedor = row["vendedor"].ToString()!,
                            Fecha = Convert.ToDateTime(row["fecha"]),
                            Forma_Pago = new FormaPago
                            {
                                Id = Convert.ToInt32(row["id_forma_pago"])
                            },
                            Detalles = new List<Detalle>()
                        };

                        facturas.Add(factura);
                        facturaIdActual = idFactura;
                    }

                    var detalle = new Detalle
                    {
                        Cabecera = factura!,
                        Id = Convert.ToInt32(row["id_detalle"]),
                        Articulo = new Articulo
                        {
                            Id = Convert.ToInt32(row["id_articulo"]),
                            Descripcion = row["descripcion"].ToString()!,
                            Stock = Convert.ToInt32(row["stock"]),
                            Precio = Convert.ToDouble(row["precio"]),
                            Activo = Convert.ToBoolean(row["activo"])
                        },
                        Cantidad = Convert.ToInt32(row["cantidad"]),
                        PrecioUnitario = Convert.ToDouble(row["pre_unitario"])
                    };

                    factura!.Detalles.Add(detalle);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las facturas", ex);
            }

            return facturas;
        }

        public Factura? GetById(int id)
        {
            Factura? factura = null;

            try
            {
                var parameters = new List<Parameters>
        {
            new Parameters { Name = "@codigo", Value = id }
        };

                var dt = DataHelper.GetInstance().ExecuteSP("SP_CONSULTAR_FACTURA_POR_ID", parameters);

                if (dt == null || dt.Rows.Count == 0)
                    return null;

                foreach (DataRow row in dt.Rows)
                {
                    if (factura == null)
                    {
                        factura = new Factura
                        {
                            Id = Convert.ToInt32(row["id_factura"]),
                            Cliente = row["cliente"] == DBNull.Value ? string.Empty : row["cliente"].ToString()!,
                            Vendedor = row["vendedor"] == DBNull.Value ? string.Empty : row["vendedor"].ToString()!,
                            Fecha = Convert.ToDateTime(row["fecha"]),
                            Forma_Pago = new FormaPago
                            {
                                Id = Convert.ToInt32(row["id_forma_pago"])
                            },
                            Detalles = new List<Detalle>()
                        };
                    }

                    var detalle = new Detalle
                    {
                        Cabecera = factura,
                        Id = Convert.ToInt32(row["id_detalle"]),
                        Articulo = new Articulo
                        {
                            Id = Convert.ToInt32(row["id_articulo"]),
                            Descripcion = row["descripcion"] == DBNull.Value ? string.Empty : row["descripcion"].ToString()!,
                            Stock = Convert.ToInt32(row["stock"]),
                            Precio = Convert.ToDouble(row["precio"]),
                            Activo = Convert.ToBoolean(row["activo"])
                        },
                        Cantidad = Convert.ToInt32(row["cantidad"]),
                        PrecioUnitario = Convert.ToDouble(row["pre_unitario"])
                    };

                    factura.Detalles.Add(detalle);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la factura por id", ex);
            }

            return factura;
        }

        public bool Save(Factura factura)
        {
            bool done = false;

            using (var cnn = DataHelper.GetInstance().GetConnection())
            {
                cnn.Open();
                using (var t = cnn.BeginTransaction())
                {
                    try
                    {
                        bool isNew = factura.Id == 0;

                        SqlCommand cmd = new SqlCommand("SP_GUARDAR_CABECERA", cnn, t);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@factura", factura.Id);
                        cmd.Parameters.AddWithValue("@CLIENTE", factura.Cliente);
                        cmd.Parameters.AddWithValue("@VENDEDOR", factura.Vendedor);
                        cmd.Parameters.AddWithValue("@FECHA", factura.Fecha);
                        cmd.Parameters.AddWithValue("@FORMAPAGO", factura.Forma_Pago.Id);

                        if (isNew)
                        {
                            SqlParameter param = new SqlParameter("@facturaout", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(param);
                        }

                        var rows = cmd.ExecuteNonQuery();
                        if (rows <= 0)
                        {
                            t.Rollback();
                            return false;
                        }

                        int facturaId = isNew ? Convert.ToInt32(cmd.Parameters["@facturaout"].Value) : factura.Id;

                        foreach (var detalle in factura.Detalles)
                        {
                            var cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                            cmdDetalle.CommandType = CommandType.StoredProcedure;
                            cmdDetalle.Parameters.AddWithValue("@factura", facturaId);
                            cmdDetalle.Parameters.AddWithValue("@ARTICULO", detalle.Articulo.Id);
                            cmdDetalle.Parameters.AddWithValue("@CANTIDAD", detalle.Cantidad);
                            cmdDetalle.Parameters.AddWithValue("@PRE_UNITARIO", detalle.PrecioUnitario);

                            var rowsDetalle = cmdDetalle.ExecuteNonQuery();
                            if (rowsDetalle <= 0)
                            {
                                t.Rollback();
                                throw new Exception("No se pudo guardar un detalle de la factura");
                            }
                        }

                        t.Commit();
                        done = true;
                    }
                    catch (Exception ex)
                    {
                        t.Rollback();
                        throw new Exception("Error al guardar la factura", ex);
                    }
                }
            }

            return done;
        }

    }
}

