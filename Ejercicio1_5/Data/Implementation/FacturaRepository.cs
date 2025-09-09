using Ejercicio1_5.Data.Helper;
using Ejercicio1_5.Data.Interface;
using Ejercicio1_5.Domain;
using Ejercicio1_5.Data.UoW;
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
    public class FacturaRepository : IFacturaRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public FacturaRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
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
            try
            {
                bool isNew = factura.Id == 0;
                var param = new List<Parameters>()
                {
                    new Parameters() { Name = "@factura", Value = factura.Id },
                    new Parameters() { Name = "@CLIENTE", Value = factura.Cliente },
                    new Parameters() { Name = "@VENDEDOR", Value = factura.Vendedor },
                    new Parameters() { Name = "@FECHA", Value = factura.Fecha },
                    new Parameters() { Name = "@FORMAPAGO", Value = factura.Forma_Pago.Id },
                    new Parameters() { Name = "@FACTURAOUT", Value = 0, IsOut = true }
                };
                var rowCabecera = DataHelper.GetInstance().ExecuteSPNonQuery("SP_GUARDAR_CABECERA", param, _connection, _transaction);
                int facturaId = isNew ? Convert.ToInt32(param.First(p => p.Name == "@FACTURAOUT").Value) : factura.Id;
                foreach (var detalle in factura.Detalles)
                {
                    var paramdetalle = new List<Parameters>()
                    {
                        new Parameters() { Name ="@factura", Value = facturaId },
                        new Parameters() { Name ="@ARTICULO", Value = detalle.Articulo.Id },
                        new Parameters() { Name ="@CANTIDAD", Value= detalle.Cantidad },
                        new Parameters() { Name ="@PRE_UNITARIO", Value=detalle.PrecioUnitario },
                    };
                    var rowsDetalle = DataHelper.GetInstance().ExecuteSPNonQuery("SP_INSERTAR_DETALLE", paramdetalle, _connection, _transaction);
                }
                done = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la factura", ex);
            }
            return done;
        }
    }
}


