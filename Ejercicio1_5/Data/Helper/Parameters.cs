using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Data.Helper
{
    public class Parameters
    {
        private bool isOut = false;
        public  required string Name { get; set; }
        public  required object Value { get; set; }
        public ParameterDirection Direction { get; set; } = ParameterDirection.Input;
        public bool IsOut { get { return isOut; } set { isOut = value; } }
    }
}
