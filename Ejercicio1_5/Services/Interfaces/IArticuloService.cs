using Ejercicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ejercicio1_5.Services.Interfaces
{
    public interface IArticuloService
    {
        bool Save(Articulo articulo);
        List<Articulo> GetAll();
        Articulo GetById(int id);
        bool Delete(int id);

    }
}
