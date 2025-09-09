using Problema1_8.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Problema1_8.Data.DataHelper;
using System.Data;
using Problema1_8.Domain;
using System.Linq.Expressions;

namespace Problema1_8.Data.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                var client = GetById(id);
                if (client == null)
                {
                    throw new Exception("Client not found with ID: " + id);
                }
                else
                {
                    DataHelper.DataHelper dh = DataHelper.DataHelper.GetInstance();
                    var dt = dh.ExecuteSP("SP_DELETECLIENT", new List<Parameters> { new Parameters("@id", id) });
                    result = dt.Rows.Count > 0 && (int)dt.Rows[0]["RowsAffected"] > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting client: " + ex.Message);
            }
            return result;
        }

        public List<Client> GetAll()
        {
            var lst = new List<Client>();
            try
            {
                DataHelper.DataHelper dh = DataHelper.DataHelper.GetInstance();
                var dr = dh.ExecuteSP("sp_GetAllClients", null);
                if (dr != null && dr.Rows.Count > 0)
                {
                    foreach (DataRow row in dr.Rows)
                    {
                        var client = new Client
                        {
                            Id = Convert.ToInt32(row["Id_client"]),
                            Name = row["Nameclient"].ToString() ?? string.Empty,
                            Gender = (bool)row["Gender"],
                            Active = (bool)row["Active"]
                        };
                        lst.Add(client);
                    }
                }
            }
            catch (Exception ex)
            {
                lst = null;
                throw new Exception("Error retrieving all clients: " + ex.Message);
            }
            return lst;
        }

        public Client GetById(int id)
        {
            var client = new Client();
            try
            {
                DataHelper.DataHelper dh = DataHelper.DataHelper.GetInstance();
                var dr = dh.ExecuteSP("sp_GetClientById",
                    new List<Parameters> { new Parameters("@Id", id) });
                if (dr != null && dr.Rows.Count > 0)
                {
                    DataRow row1 = dr.Rows[0];
                    client.Id = Convert.ToInt32(row1["Id_client"]);
                    client.Name = row1["Nameclient"].ToString() ?? string.Empty;
                    client.Gender = (bool)row1["Gender"];
                    client.Active = (bool)row1["Active"];
                }
                else
                {
                    client = null;
                    {
                        throw new Exception("Client not found with ID: " + id);
                    }
                }
            }
            catch (Exception ex)
            {
                client = null;
                throw new Exception("Error retrieving client by ID: " + ex.Message);
            }
            return client;
        }

        public bool Insert(Client entity)
        {
            var dh = DataHelper.DataHelper.GetInstance();
            bool result = false;
            try
            {
                var dt = dh.ExecuteSP("SP_SAVECLIENT", new List<Parameters>
                    {
                        new Parameters("@Id_client", 0),
                        new Parameters("@Nameclient", entity.Name),
                        new Parameters("@Gender", entity.Gender),
                        new Parameters("@Active", entity.Active)
                    });
                result = dt.Rows.Count > 0 && (int)dt.Rows[0]["RowsAffected"] > 0;

            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting client: " + ex.Message);
            }
            return result;
        }

        public bool Update(Client entity)
        {
            var dh = DataHelper.DataHelper.GetInstance();
            bool result = false;
            try
            {
                var dt = dh.ExecuteSP("SP_SAVECLIENT", new List<Parameters>
                {
                        new Parameters("@Id_client", entity.Id),
                        new Parameters("@Nameclient", entity.Name),
                        new Parameters("@Gender", entity.Gender),
                        new Parameters("@Active", entity.Active)
                });
                result = dt.Rows.Count > 0 && (int)dt.Rows[0]["RowsAffected"] > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating client: " + ex.Message);
            }
            return result;
        }

        public bool AddPetToClient(Client client)
        {
            var dh = DataHelper.DataHelper.GetInstance();
            bool result = false;

            try
            {
                int count = 0;
                foreach (var pet in client.Pets)
                {
                    List<Parameters> parameters = new List<Parameters>();
                    int petId = 0;
                    Parameters clientparam = new Parameters("@Id_pet", client.Pets[petId].Id);
                    Parameters petparam = new Parameters("@Id_client", client.Id);
                    petId++;
                    parameters.Add(clientparam);
                    parameters.Add(petparam);
                    var dt = dh.ExecuteSP("SP_ADDPETTOCLIENT", parameters);
                    if (result = dt.Rows.Count > 0 && (int)dt.Rows[0]["RowsAffected"] > 0)
                    {
                        count++;
                    }
                }
                result = count == client.Pets.Count;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding pet to client: " + ex.Message);
            }
            return result;
        }
        public bool AddPetToClient(Pet pet, Client client)
        {
            var dh = DataHelper.DataHelper.GetInstance();
            bool result = false;

            try
            {
                var dt = dh.ExecuteSP("SP_ADDPETTOCLIENT", new List<Parameters>
                {
                    new Parameters("@Id_pet", pet.Id),
                    new Parameters("@Id_client", client.Id)
                });
                result = dt.Rows.Count > 0 && (int)dt.Rows[0]["RowsAffected"] > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding pet to client: " + ex.Message);
            }
            return result;
        }
    }
}
