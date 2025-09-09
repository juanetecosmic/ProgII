using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_8.Domain
{
    public class Client
    {
        private int id;
        private string name;
        private bool gender;
        private List<Pet> pets;
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; } // True = Masculino, False = Feminino
        public List<Pet> Pets { get; set; }
        public bool Active { get; set; }
        public Client()
        {
            Pets = new List<Pet>();
        }
        public override string ToString()
        {
            string g = gender ? "Masculino" : "Femenino";
            string petList = string.Empty;
            foreach (var pet in Pets)
            {
                petList += $"\n - {pet.ToString()}";
            }
            return $"\nId: {Id}, \nName: {Name}" +
                $"\n Gender: {g}," +
                $"\n Pet(s): " + petList;
        }
    }
}
