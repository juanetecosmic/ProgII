// See https://aka.ms/new-console-template for more information
using Ejercicio1_5.Domain;
using Ejercicio1_5.Services;


ArticuloService a = new ArticuloService();
//Crear producto N°1
Console.WriteLine("\nCreando producto 1");
Articulo art = new Articulo() { Id = 0, Descripcion = "Papas fritas Lays 500 g", Stock = 20, Precio = 7000, Activo = true };
if (a.Save(art))
{
    Console.WriteLine("Producto guardado exitosamente");
}
else
{
    Console.WriteLine("Error al guardar el producto");
}

//Crear producto N°2
Console.WriteLine("\nCreando producto 2");
Articulo art2 = new Articulo() {Id = 0, Descripcion = "Coca Cola 1.5 L", Stock = 15, Precio = 8000, Activo = true };

if (a.Save(art2))
{
    Console.WriteLine("Producto guardado exitosamente");
}
else
{
    Console.WriteLine("Error al guardar el producto");
}

//Listar productos
Console.WriteLine("\nListando todos los productos");
var todos = a.GetAll();
if (todos.Count == 0)
{
    Console.WriteLine("No hay productos para mostrar");
}
else
{
    foreach (Articulo articulo in todos)
    {
        Console.WriteLine(articulo.ToString());
    }
}



