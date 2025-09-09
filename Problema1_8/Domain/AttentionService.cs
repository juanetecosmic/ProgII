using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_8.Domain
{
    public class AttentionService
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public override string ToString()
        {
            return $"{Description} - {Price:C}";
        }
    }
}
