using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_3
{
    public class Producto : IPrecio
    {
        private int codigo;
        private string nombre;
        private double precio;
        public  int pCodigo { get; set; }
        public string pNombre { get; set; }
        public double pPrecio { get; set; }

        public double calcularPrecio(double d)
        {
            double nuevoprecio;
            return nuevoprecio = precio*d;
        }
    }
}
