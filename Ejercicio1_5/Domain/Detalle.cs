using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Domain
{
    public class Detalle
    {
        public int Id { get; set; }
        public required Factura Cabecera { get; set; }
        public required Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
        public double Importe { get; set; }
        public Detalle()
        {
            Id = 0;
            Cantidad = 0;
            Importe = 0;
        }
        public double CalculateSubtotal()
        {
            var subtotal = Importe * Cantidad;
            return subtotal;
        }
        public override string ToString()
        {
            return $"\nArtículo = {Articulo.Descripcion} \n" +
                $"Cantidad = {Cantidad}\n" +
                $"Importe = {Importe}\n";
        }
    }
}
