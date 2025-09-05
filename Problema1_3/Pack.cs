using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_3
{
    public class Pack:Producto
    {
        private int cantidad;
        public double pCantidad { get; set; }
        public Pack():base()
        {
            pCodigo = 0;
            pNombre = string.Empty;
            pPrecio = base.calcularPrecio(cantidad*base.pPrecio);
            cantidad = 0;
        }
    }
}
