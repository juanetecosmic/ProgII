using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_8.Domain
{
    public class Attention
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Pet Pet { get; set; }
        public List<AttentionDetail> Details { get; set; }
        public decimal Discount { get; set; }
        public bool Active { get; set; }
        public decimal Total()
        {
            decimal d = 0;
            foreach (var item in Details)
            {
                d += item.SubTotal();
            }
            return d;
        }
        public decimal TotalWithDiscount()
        {
            return Total() - Discount;
        }
        public Attention()
        {
            Details = new List<AttentionDetail>();
        }
        public void AddDetail(AttentionDetail detail)
        {
            Details.Add(detail);
        }
        public void RemoveDetail(int detailId)
        {
            if(detailId >= 0 && detailId <= Details.Count)
                Details.RemoveAt(detailId);
            else
                throw new ArgumentOutOfRangeException("detailId", "Detail ID is out of range.");
        }
        public override string ToString()
        {
            string detailList = string.Empty;
            foreach (var detail in Details)
            {
                detailList += $"\n - {detail.ToString()}";
            }
            return $"\nId: {Id}," +
                $"\nDate: {Date.ToShortDateString()}," +
                $"\nClient: {Client.Name}," +
                $"\nPet: {Pet.Name}," +
                $"\nDetail(s): " + detailList +
                $"\nDiscount: {Discount:C}," +
                $"\nTotal: {Total():C}," +
                $"\nTotal with Discount: {TotalWithDiscount():C}\n";
        }

    }
}
