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
    public class AttentionServiceService
    {
        private IRepository<AttentionService> _attentionServiceRepository;
        public AttentionServiceService()
        {
            _attentionServiceRepository = new AttentionServiceRepository();
        }
        public List<AttentionService> GetAllServices()
        {
            return _attentionServiceRepository.GetAll();
        }
        public AttentionService GetServiceById(int id)
        {
            return _attentionServiceRepository.GetById(id);
        }
        public bool AddService(AttentionService service)
        {
            return _attentionServiceRepository.Insert(service);
        }
        public bool UpdateService(AttentionService service)
        {
            return _attentionServiceRepository.Update(service);
        }
        public bool DeleteService(int id)
        {
            return _attentionServiceRepository.Delete(id);
        }
    }
}
