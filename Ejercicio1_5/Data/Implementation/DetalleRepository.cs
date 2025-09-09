using Ejercicio1_5.Data.Helper;
using Ejercicio1_5.Data.Interface;
using Ejercicio1_5.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio1_5.Data.UoW;
using System.Data;
using Microsoft.Identity.Client;

namespace Ejercicio1_5.Data.Implementation
{
    public class DetalleRepository : IDetalleRepository
    {

        public List<Detalle> GetDetalles(int facturaId)
        {
            List<Detalle> detalles = new List<Detalle>();
            try
            {
                List<Parameters> param = new List<Parameters> { new Parameters { Name = "@codigo", Value = facturaId } };
                var dt = DataHelper.GetInstance().ExecuteSP("SP_CONSULTAR_DETALLES_POR_FACTURA_ID", param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Detalle a = new Detalle
                        {
                            Articulo = new Articulo
                            {
                                Id = (int)row["id_articulo"],
                                Descripcion = row["descripcion"].ToString() ?? string.Empty
                            },
                            PrecioUnitario = Convert.ToDouble(row["pre_unitario"]),
                            Cantidad = (int)row["cantidad"]
                        };
                        detalles.Add(a);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error trayendo detalles", ex);
            }
            return detalles;
        }

    }
}
