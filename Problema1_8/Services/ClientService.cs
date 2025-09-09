using Problema1_8.Data.Interfaces;
using Problema1_8.Domain;
using Problema1_8.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_8.Services
{
    public class ClientService
    {
        private IRepository<Client> _clientRepository;
        public ClientService()
        {
            _clientRepository = new ClientRepository();
        }
        public List<Client> GetAllClients()
        {
            return _clientRepository.GetAll();
        }
        public Client GetClientById(int id)
        {
            return _clientRepository.GetById(id);
        }
        public bool AddClient(Client client)
        {
            return _clientRepository.Insert(client);
        }
        public bool UpdateClient(Client client)
        {
            return _clientRepository.Update(client);
        }
        public bool DeleteClient(int id)
        {
            return _clientRepository.Delete(id);
        }
        public bool AddPetToClient(Pet pet, Client client)
        {
            var c = new ClientRepository();
            return c.AddPetToClient(pet, client);
        }
    }
}
