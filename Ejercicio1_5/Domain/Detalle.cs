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
        public Factura Cabecera { get; set; }
        public required Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        
        public double CalculateSubtotal()
        {
            var subtotal = PrecioUnitario * Cantidad;
            return subtotal;
        }
        public override string ToString()
        {
            return $"\nArtículo = {Articulo.Descripcion} \n" +
                $"Cantidad = {Cantidad}\n" +
                $"Precio unitario = {PrecioUnitario}\n";
        }
    }
}
