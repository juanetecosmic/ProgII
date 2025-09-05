using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_3
{
    public class Suelto:Producto
    {
        private int medida;
        public double pMedida { get; set; }
        public Suelto() : base()
        {
            pCodigo = 0;
            pNombre = string.Empty;
            pPrecio = base.calcularPrecio(medida * base.pPrecio);
            medida = 0;
        }
    }
}
