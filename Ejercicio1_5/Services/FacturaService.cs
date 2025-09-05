using Ejercicio1_5.Data.Implementation;
using Ejercicio1_5.Data.Interface;
using Ejercicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Services
{
    public class FacturaService
    {
        private IFacturaRepository _facturaRepository;
        public FacturaService()
        {
            _facturaRepository = new FacturaRepository();
        }
        public bool Save(Factura factura)
        {
            return _facturaRepository.Save(factura);
        }
        public List<Factura> GetAll()
        {
            return _facturaRepository.GetAll();
        }
        public Factura? GetById(int id)
        {
            return _facturaRepository.GetById(id);
        }
    }
}
