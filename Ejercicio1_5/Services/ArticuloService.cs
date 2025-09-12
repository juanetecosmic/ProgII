using Ejercicio1_5.Data;
using Ejercicio1_5.Data.Implementation;
using Ejercicio1_5.Data.Interface;
using Ejercicio1_5.Domain;
using Ejercicio1_5.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Services
{
    public class ArticuloService : IArticuloService
    {
        private IArticuloRepository _articuloRepository;
        public ArticuloService()
        {
            _articuloRepository = new ArticuloRepository();
        }
        public bool Save(Articulo articulo)
        {
            return _articuloRepository.Save(articulo);
        }
        public List<Articulo> GetAll()
        {
            return _articuloRepository.GetAll();
        }
        public Articulo? GetById(int id)
        {
            return _articuloRepository.GetById(id);
        }
        public bool Delete(int id)
        {
            return _articuloRepository.Delete(id);
        }
    }
}
