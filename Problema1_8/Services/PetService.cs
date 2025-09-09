using Problema1_8.Data.Interfaces;
using Problema1_8.Data.Repositories;
using Problema1_8.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_8.Services
{
    public class PetService
    {
        private IPetRepository _petRepository;
        public PetService()
        {
            _petRepository = new PetRepository();
        }
        public List<Pet> GetAllPets()
        {
            return _petRepository.GetAll();
        }
        public Pet GetPetById(int id)
        {
            return _petRepository.GetById(id);
        }
        public bool AddPet(Pet pet)
        {
            return _petRepository.Insert(pet);
        }
        public bool UpdatePet(Pet pet)
        {
            return _petRepository.Update(pet);
        }
        public bool DeletePet(int id)
        {
            return _petRepository.Delete(id);
        }
        public List<Pet> GetPetsByClientId(int clientId)
        {
            return _petRepository.GetPetsByClientId(clientId);
        }
    }
}
