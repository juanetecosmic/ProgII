using Ejercicio1_5.Data.Helper;
using Ejercicio1_5.Data.Implementation;
using Ejercicio1_5.Data.Interface;
using Ejercicio1_5.Data.UoW;
using Ejercicio1_5.Domain;
using Ejercicio1_5.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Services
{
    public class FacturaService: IFacturaService
    {
        private readonly UnitOfWork _unitOfWork;

        public FacturaService()
        {
            if (_unitOfWork == null)
            {
                _unitOfWork = new UnitOfWork();
            }
            _facturaRepository = new FacturaRepository(DataHelper.GetInstance().GetConnection());
            _detalleRepository = new DetalleRepository();
        }
        private IFacturaRepository _facturaRepository;
        private IDetalleRepository _detalleRepository;

        public List<Factura> GetAll()
        {
            return _facturaRepository.GetAll();
        }

        public Factura? GetById(int id)
        {

            var a = _facturaRepository.GetById(id);
            List<Detalle> b = _detalleRepository.GetDetalles(id);
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
