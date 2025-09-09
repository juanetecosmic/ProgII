using Problema1_8.Data.DataHelper;
using Problema1_8.Data.Interfaces;
using Problema1_8.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_8.Data.Repositories
{
    public class AttentionServiceRepository : IRepository<AttentionService>
    {
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                var serv = GetById(id);
                if (serv == null)
                {
                    throw new Exception("Service not found with ID: " + id);
                }
                else
                {
                    DataHelper.DataHelper dh = DataHelper.DataHelper.GetInstance();
                    var dt = dh.ExecuteSP("SP_DELETESERVICE", new List<Parameters> { new Parameters("@id", id) });
                    result = dt.Rows.Count > 0 && (int)dt.Rows[0]["RowsAffected"] > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting service: " + ex.Message);
            }
            return result;
        }

        public List<AttentionService> GetAll()
        {
            var lst = new List<AttentionService>();
            try
            {
                DataHelper.DataHelper dh = DataHelper.DataHelper.GetInstance();
                var dr = dh.ExecuteSP("sp_GetAllServices", null);
                if (dr != null && dr.Rows.Count > 0)
                {
                    foreach (DataRow row in dr.Rows)
                    {
                        var service = new AttentionService
                        {
                            Id = Convert.ToInt32(row["Id_service"]),
                            Description = row["ServiceDescription"].ToString() ?? string.Empty,
                            Price = (decimal)row["Price"],
                            Active = (bool)row["Active"]
                        };
                        lst.Add(service);
                    }
                }
            }
            catch (Exception ex)
            {
                lst = null;
                throw new Exception("Error retrieving all services: " + ex.Message);
            }
            return lst;
        }

        public AttentionService GetById(int id)
        {
            var service = new AttentionService();
            try
            {
                DataHelper.DataHelper dh = DataHelper.DataHelper.GetInstance();
                var dr = dh.ExecuteSP("sp_GetServiceById",
                    new List<Parameters> { new Parameters("@Id", id) });
                if (dr != null && dr.Rows.Count > 0)
                {
                    DataRow row1 = dr.Rows[0];
                    service.Id = Convert.ToInt32(row1["Id_client"]);
                    service.Description = row1["ServiceDescription"].ToString() ?? string.Empty;
                    service.Price = (decimal)row1["Price"];
                    service.Active = (bool)row1["Active"];
                }
                else
                {
                    service = null;
                    {
                        throw new Exception("Service not found with ID: " + id);
                    }
                }
            }
            catch (Exception ex)
            {
                service = null;
                throw new Exception("Error retrieving service by ID: " + ex.Message);
            }
            return service;
        }

        public bool Insert(AttentionService entity)
        {
            var dh = DataHelper.DataHelper.GetInstance();
            bool result = false;
            try
            {
                var dt = dh.ExecuteSP("SP_SAVESERVICE", new List<Parameters>
                    {
                        new Parameters("@Id_service", 0),
                        new Parameters("@ServiceDescription", entity.Description),
                        new Parameters("@Price", entity.Price),
                        new Parameters("@Active", entity.Active)
                    });
                result = dt.Rows.Count > 0 && (int)dt.Rows[0]["RowsAffected"] > 0;

            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting service: " + ex.Message);
            }
            return result;
        }

        public bool Update(AttentionService entity)
        {
            var dh = DataHelper.DataHelper.GetInstance();
            bool result = false;
            try
            {
                var dt = dh.ExecuteSP("SP_SAVESERVICE", new List<Parameters>
                    {
                        new Parameters("@Id_service", entity.Id),
                        new Parameters("@ServiceDescription", entity.Description),
                        new Parameters("@Price", entity.Price),
                        new Parameters("@Active", entity.Active)
                    });
                result = dt.Rows.Count > 0 && (int)dt.Rows[0]["RowsAffected"] > 0;

            }
            catch (Exception ex)
            {
                throw new Exception("Error updating service: " + ex.Message);
            }
            return result;
        }
    }
}
