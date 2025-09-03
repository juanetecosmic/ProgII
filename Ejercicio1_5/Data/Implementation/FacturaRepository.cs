using Ejercicio1_5.Data.Helper;
using Ejercicio1_5.Data.Interface;
using Ejercicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Data.Implementation
{
    public class FacturaRepository : IFacturaRepository
    {
        public List<Factura> GetAll()
        {
            throw new NotImplementedException();
        }

        public Factura? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int NextId()
        {
            throw new NotImplementedException();
        }

        public bool Save(Factura factura)
        {
            var dh= DataHelper.GetInstance().ExecuteSP("SP_INSERTAR_CABECERA");
            
        }
    }
}
