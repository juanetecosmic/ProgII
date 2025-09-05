using Ejercicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Data.Interface
{
    public interface IFacturaRepository
    {
        bool Save(Factura factura);
        List<Factura> GetAll();
        Factura? GetById(int id);
    }
}
