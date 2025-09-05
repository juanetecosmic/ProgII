using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Domain
{
    public class Factura
    {
        public  int Id { get; set; }           
        public  required string Cliente { get; set; }           
        public  required string Vendedor { get; set; }           
        public  DateTime Fecha { get; set; }
        public  FormaPago Forma_Pago { get; set; }
        private List<Detalle> detalles;
        public List<Detalle> Detalles { get; set; }
        public List<Detalle> GetDetalles() => Detalles;
        public Factura()
        {
            Fecha = DateTime.Now;
            Detalles = new List<Detalle>();
        }
        public void AddDetalle(Detalle detalle)
        {
            Detalles.Add(detalle);
        }
        public void RemoveDetalle(int indice)
        {
            Detalles.RemoveAt(indice);
        }
        
        public double Total()
        {
            double total = 0;
            foreach (var detalle in Detalles)
            {
                total += detalle.CalculateSubtotal();
            }
            return total;
        }
        public override string ToString()
        {
            string detallestring = string.Empty;
            foreach (var detalle in Detalles)
            {
                detallestring = detalle.ToString() + "\nSubtotal = " +
                detalle.CalculateSubtotal();
            }
            return $"\nVALIDEZ SOLO POR 48 HORAS\n" +
                $"Código = {Id} \n" +
                $"Cliente = {Cliente} \n" +
                $"Vendedor = {Vendedor}\n" +
                $"Fecha = {Fecha}\n" +
                $"Forma de Pago = {Forma_Pago.ToString()}\n" +
                $"Detalle:\n" + detallestring + "\n" +
                "Total: " + Total();
        }
    }
}
