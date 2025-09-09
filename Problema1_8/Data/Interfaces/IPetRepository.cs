using Problema1_8.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_8.Data.Interfaces
{
    public interface IPetRepository
    {
        bool Insert(Pet entity);
        bool Update(Pet entity);
        bool Delete(int id);
        Pet GetById(int id);
        List<Pet> GetAll();
        List<Pet> GetPetsByClientId(int clientId);

    }
}
