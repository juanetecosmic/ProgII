using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_8.Domain
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public PetType Type { get; set; }
        public bool Active { get; set; }
        public override string ToString()
        {
            return $"\nId: {Id}," +
                $"\nName: {Name}," +
                $" \nAge: {Age}," +
                $" \nType: {Type.Description}\n";
        }

    }
}
