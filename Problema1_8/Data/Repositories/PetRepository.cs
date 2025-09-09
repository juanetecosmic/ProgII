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
    public class PetRepository : IPetRepository
    {
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                var pet = GetById(id);
                if (pet == null)
                {
                    throw new Exception("Pet not found with ID: " + id);
                }
                else
                {
                    DataHelper.DataHelper dh = DataHelper.DataHelper.GetInstance();
                    var dt = dh.ExecuteSP("SP_DELETepeT", new List<Parameters> { new Parameters("@id", id) });
                    result = dt.Rows.Count > 0 && (int)dt.Rows[0]["RowsAffected"] > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting pet: " + ex.Message);
            }
            return result;
        }

        public List<Pet> GetAll()
        {
            var lst = new List<Pet>();
            try
            {
                DataHelper.DataHelper dh = DataHelper.DataHelper.GetInstance();
                var dr = dh.ExecuteSP("sp_GetAllPets", null);
                if (dr != null && dr.Rows.Count > 0)
                {
                    foreach (DataRow row in dr.Rows)
                    {
                        var petTypes = new Dictionary<int, PetType>(); // Dictionary to hold PetTypes by their ID
                        if (!petTypes.ContainsKey((int)row["ID_petType"])) // Check if PetType already exists in dictionary
                        {
                            var pt = petTypes[(int)row["ID_petType"]] = new PetType()// Create and add PetType if not already in dictionary 
                            {
                                Id = (int)row["ID_petType"],
                                Description = row["descriptiontype"].ToString() ?? String.Empty
                            };
                        }
                        var pet = new Pet()
                        {
                            Id = Convert.ToInt32(row["Id_pet"]),
                            Name = row["Namepet"].ToString() ?? string.Empty,
                            Age = Convert.ToInt32(row["Age"]),
                            Type = petTypes[(int)row["ID_petType"]], // Assign PetType from dictionary
                            Active = (bool)row["Active"] ? true : false,
                        };
                        lst.Add(pet);
                    }
                }
            }
            catch (Exception ex)
            {
                lst = null;
                throw new Exception("Error retrieving all pets: " + ex.Message);
            }
            return lst;
        }

        public Pet GetById(int id)
        {
            Pet pet = new Pet();
            var dh = DataHelper.DataHelper.GetInstance();
            var dt = dh.ExecuteSP("sp_GetPetById", new List<Parameters> { new Parameters("@Id", id) });
            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                pet.Id = Convert.ToInt32(row["Id_pet"]);
                pet.Name = row["Namepet"].ToString() ?? string.Empty;
                pet.Age = Convert.ToInt32(row["Age"]);
                pet.Type = new PetType()
                {
                    Id = (int)row["ID_petType"],
                    Description = row["descriptiontype"].ToString() ?? String.Empty
                };
                pet.Active = (bool)row["Active"];
            }
            else
            {
                throw new Exception("Pet not found with ID: " + id);
            }
            return pet;
        }

        public List<Pet> GetPetsByClientId(int clientId)
        {
            var pets = new List<Pet>();
            try
            {
                DataTable dt = new DataTable();
                DataHelper.DataHelper dh = DataHelper.DataHelper.GetInstance();
                var drpets = dh.ExecuteSP("sp_GetPetsByClientId",
                    new List<Parameters> { new Parameters("@ClientId", clientId) });
                if (drpets != null && drpets.Rows.Count > 0)
                {
                    foreach (DataRow row in drpets.Rows)
                    {
                        Pet pet = new Pet()
                        {
                            Id = Convert.ToInt32(row["Id_pet"]),
                            Name = row["Namepet"].ToString() ?? string.Empty,
                            Age = Convert.ToInt32(row["Age"]),
                            Type = new PetType
                            {
                                Id = (int)row["ID_petType"],
                                Description = row["descriptiontype"].ToString() ?? String.Empty
                            },
                            Active = (bool)row["Active"]
                        };
                        pets.Add(pet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving pets by client ID: " + ex.Message);
            }
            return pets;
        }

        public bool Insert(Pet entity)
        {
            var dh = DataHelper.DataHelper.GetInstance();
            bool result = false;
            try
            {
                var dt = dh.ExecuteSP("SP_SAVEPET", new List<Parameters>
                {
                        new Parameters("@Id_pet", 0),
                        new Parameters("@Namepet", entity.Name),
                        new Parameters("@Age", entity.Age),
                        new Parameters("@ID_petType", entity.Type.Id),
                        new Parameters("@Active", entity.Active)
                });
                result = dt.Rows.Count > 0 && (int)dt.Rows[0]["RowsAffected"] > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating pet: " + ex.Message);
            }
            return result;
        }

        public bool Update(Pet entity)
        {
            var dh = DataHelper.DataHelper.GetInstance();
            bool result = false;
            try
            {
                var dt = dh.ExecuteSP("SP_SAVEPET", new List<Parameters>
                {
                        new Parameters("@Id_pet", entity.Id),
                        new Parameters("@Namepet", entity.Name),
                        new Parameters("@Age", entity.Age),
                        new Parameters("@ID_petType", entity.Type.Id),
                        new Parameters("@Active", entity.Active)
                });
                result = dt.Rows.Count > 0 && (int)dt.Rows[0]["RowsAffected"] > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating pet: " + ex.Message);
            }
            return result;
        }
    }
}
