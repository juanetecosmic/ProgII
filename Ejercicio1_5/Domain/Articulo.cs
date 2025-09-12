using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Domain
{
    public class Articulo
    {
        private int id;
        private string descripcion;
        private int stock;
        private double precio;
        private bool activo;
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public double Precio { get; set; }
        public bool Activo { get; set; }
        public Articulo()
        {
            Id = 0;
            Descripcion = string.Empty;
            Stock = 0;
            Precio = 0;
            Activo = true;
        }
        //public override string ToString()
        //{
        //    return $"\nCódigo = {Id} \n" +
        //        $"Descripción = {Descripcion} \n" +
        //        $"Stock = {Stock}\n" +
        //        $"Precio = {Precio}\n" +
        //        $"Activo = {Activo}\n";
        //}

    }
}
