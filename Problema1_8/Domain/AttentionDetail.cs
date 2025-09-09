using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_8.Domain
{
    public class AttentionDetail
    {
        public Attention Attention { get; set; }

        public AttentionService Service { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal()
        { 
                return Price * Quantity;
        }
        public override string ToString()
        {
            return $"\nService: {Service.ToString()}," +
                $"\nPrice: {Price:C}," +
                $"\nQuantity: {Quantity}," +
                $"\nSubTotal: {SubTotal()}\n";
        }
    }
}
