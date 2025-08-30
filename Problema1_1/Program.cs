// See https://aka.ms/new-console-template for more information
using System.Collections;
using Problema1_1;

Console.WriteLine("Hello, World!");
Problema1_1.ICollection pila = new Pila(10);
Console.WriteLine("¿Está vacía la pila?");
Console.WriteLine(pila.IsEmpty());
Console.WriteLine("Agregando un objeto a la pila... Ingrese código");
if (int.TryParse(Console.ReadLine(), out int result))
{
    Console.WriteLine("Entrada válida: " + result);
    pila.Add(new Pruebas(result));
}
else
{
    Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero.");
}
Console.WriteLine("¿Está vacía la pila?");
Console.WriteLine(pila.IsEmpty());
Console.WriteLine("Primer objeto en la pila:");
Console.WriteLine(pila.First());
Console.WriteLine("¿Desea eliminar el primer objeto en la pila? \n 1: Sí // 2: No");
if (int.TryParse(Console.ReadLine(), out int respuestaInt) && respuestaInt == 1)
{
    Console.WriteLine("Eliminando el primer objeto en la pila...");
    Console.WriteLine(pila.Extract());
}
else
{
    Console.WriteLine("No se eliminó ningún objeto.");
}
