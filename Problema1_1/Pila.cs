using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_1
{
    public class Pila : ICollection
    {
        public object[] items { get; set; }
        public int count { get; set; }
        public Pila(int tamaño)
        {
            items = new object[tamaño];
            count = 0;
        }

        public bool Add(object obj)
        {
            try
            {
                if (obj != null)
                {
                    if (count < items.Length)
                    {
                        items[count] = obj;
                        count++;
                        return true;
                    }
                    else
                    {
                        throw new StackOverflowException("La pila está llena.");
                    }
                }
                else
                {
                    throw new ArgumentNullException("El objeto no puede ser nulo.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el objeto: {ex.Message}");
                return false;
            }
        }

        public object Extract()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("La pila está vacía.");
            }
            else
            {
                count--;
                object obj = items[count];
                items[count] = null; // Limpiar la referencia
                return obj;
            }
        }

        public object First()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("La pila está vacía.");
            }
            else
            {
                return items[count - 1];
            }
        }

        public bool IsEmpty()
        {
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
