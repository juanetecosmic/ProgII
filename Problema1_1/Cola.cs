using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_1
{
    public class Cola : ICollection
    {
        public required List<object> elements { get; set; }
        public int count { get; set; }
        public Cola()
        {
            elements = new List<object>();
            count = 0;
        }
        public bool Add(object obj)
        {
            try
            {
                if (obj != null)
                {
                    elements.Add(obj);
                    count++;
                    return true;
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
            try {
                if (!IsEmpty())
                {
                    object obj = elements[0];
                    elements[0] = null; // Limpiar la referencia
                    count--;
                    return obj;
                }
                else
                {
                    throw new InvalidOperationException("La cola está vacía.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al extraer el objeto: {ex.Message}");
                return null;
            }
        }

        public object First()
        {
            try
            {
                if (!IsEmpty())
                {
                    return elements[0];
                }
                else
                {
                    throw new InvalidOperationException("La cola está vacía.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el primer objeto: {ex.Message}");
                return null;
            }
        }

        public bool IsEmpty()
        {
            try
            {
                return count == 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar si la cola está vacía: {ex.Message}");
                return true;
            }
        }
    }
}
