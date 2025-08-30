using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_1
{
    public class Pruebas
    {
        public  int codigo { get; set; }
        public Pruebas(int codigo)
        {
            this.codigo = codigo;
        }

        public override string ToString()
        {
            return codigo.ToString();
        }
    }
}
