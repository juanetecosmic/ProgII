using Ejercicio1_5.Data.Helper;
using Ejercicio1_5.Data.Implementation;
using Ejercicio1_5.Data.Interface;
using Ejercicio1_5.Data.UoW;
using Ejercicio1_5.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Services
{
    public class FacturaService
    {
        private readonly UnitOfWork _unitOfWork;

        public FacturaService()
        {
            if (_unitOfWork == null)
            {
                _unitOfWork = new UnitOfWork();
            }
        }

        public List<Factura> GetAll()
        {
            return _unitOfWork.FacturaRepository.GetAll();
        }

        public Factura? GetById(int id)
        {

            var a = _unitOfWork.FacturaRepository.GetById(id);
            DetalleRepository detalleRepository = new DetalleRepository();
            List<Detalle> b = detalleRepository.GetDetalles(id);
            foreach (Detalle d in b)
            {
                a.AddDetalle(d);
            }
            return a;
        }
        public bool Save(Factura factura)
        {
            var result = _unitOfWork.FacturaRepository.Save(factura);
            if (result)
                _unitOfWork.SaveChanges();
            return result;
        }
    }
}
