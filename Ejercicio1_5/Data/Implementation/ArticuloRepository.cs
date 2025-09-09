using Ejercicio1_5.Data.Helper;
using Ejercicio1_5.Data.Interface;
using Ejercicio1_5.Data.UoW;
using Ejercicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Data.Implementation
{
    public class ArticuloRepository : IArticuloRepository
    {
        public bool Delete(int id)
        {
            try
            {
                var valid = GetById(id);
                if (valid != null && valid.Id > 0)
                {
                    List<Parameters> p = new List<Parameters>()
                    { new Parameters() { Name="@codigo",Value= valid.Id} };
                    var dt = DataHelper.GetInstance().ExecuteSPNonQuery("sp_baja_articulo", p);
                    return dt;
                }
                else
                {
                    throw new Exception("El artículo no existe");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error al dar de baja el artículo", ex);
            }
        }

        public List<Articulo> GetAll()
        {
            List<Articulo> lst = new List<Articulo>();
            try
            {
                var o = DataHelper.GetInstance().ExecuteSP("sp_consultar_articulos", null);
                if (o == null )
                {
                    throw new Exception("Ocurrió un error al consultar los artículos");                     
                }
                else
                {
                    foreach (DataRow datarow in o.Rows)
                    {
                        Articulo a = new Articulo();
                        a.Id = (int)datarow["id_articulo"];
                        a.Descripcion = datarow["descripcion"].ToString() ?? string.Empty;
                        a.Stock = (int)datarow["stock"];
                        a.Precio = Convert.ToDouble(datarow["precio"]);
                        if (Convert.ToInt32(datarow["activo"]) == 1)
                        {
                            a.Activo = true;
                        }
                        else
                        {
                            a.Activo = false;
                        }
                        lst.Add(a);
                    }
                    return lst;
                }                
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error", ex);
            }
        }


        public Articulo? GetById(int id)
        {
            Articulo? a = new Articulo();
            try
            {
                List<Parameters> p = new List<Parameters>()
                { new Parameters() { Name = "@codigo", Value = id } };
                var o = DataHelper.GetInstance().ExecuteSP("sp_consultar_articulo_por_id", p);
                if (o != null && o.Rows.Count >= 0)
                {
                    DataRow datarow = o.Rows[0];
                    a.Id = datarow["id_articulo"] != DBNull.Value ? Convert.ToInt32(datarow["id_articulo"]) : 0;
                    a.Descripcion = datarow["descripcion"] != DBNull.Value ? datarow["descripcion"].ToString() : string.Empty;
                    a.Stock = datarow["stock"] != DBNull.Value ? Convert.ToInt32(datarow["stock"]) : 0;
                    a.Precio = datarow["precio"] != DBNull.Value ? Convert.ToDouble(datarow["precio"]) : 0;
                    if (Convert.ToInt32(datarow["activo"]) == 1)
                    {
                        a.Activo = true;
                    }
                    else
                    {
                        a.Activo = false;
                    }
                }
                else
                { a = null; }
            }
            catch (Exception)
            {
                throw;
            }
            return a;
        }

        public bool Save(Articulo articulo)
        {
            if (articulo.Id > 0)
            {
                try
                {
                    if (GetById(articulo.Id) != null)
                    {
                        List<Parameters> p = new List<Parameters>()
                    {
                        new Parameters() { Name = "@codigo", Value = articulo.Id },
                        new Parameters() { Name = "@descripcion", Value = articulo.Descripcion },
                        new Parameters() { Name = "@stock", Value = articulo.Stock },
                        new Parameters() { Name = "@precio", Value = articulo.Precio },
                    };
                        var o = DataHelper.GetInstance().ExecuteSPNonQuery("sp_guardar_articulo", p);
                        return o;
                    }
                    else
                    {
                        throw new Exception("El artículo no existe");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    List<Parameters> p = new List<Parameters>()
                    {
                        new Parameters() {Name = "@codigo", Value = 0 },
                        new Parameters() { Name = "@descripcion", Value = articulo.Descripcion },
                        new Parameters() { Name = "@stock", Value = articulo.Stock },
                        new Parameters() { Name = "@precio", Value = articulo.Precio },
                    };
                    var o = DataHelper.GetInstance().ExecuteSPNonQuery("sp_guardar_articulo", p);
                    return o;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}