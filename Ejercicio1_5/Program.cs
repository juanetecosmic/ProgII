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
art = a.GetById(1); 

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
art2= a.GetById(2);

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

//Insertar factura
Console.WriteLine("\nCreando factura");
FacturaService f = new FacturaService();
Factura factura = new Factura() { Cliente = "Juanito", Vendedor = "Mercedes", Forma_Pago = new FormaPago() { Id = 1, Forma_Pago = "Crédito" },
Fecha = DateTime.Now, Detalles = new List<Detalle>() };

Detalle d = new Detalle() { Articulo = art, Cantidad=2, PrecioUnitario=7000 };
Detalle d2 = new Detalle() { Articulo = art2, Cantidad=1, PrecioUnitario=8000 };
factura.AddDetalle(d);
factura.AddDetalle(d2);
if (f.Save(factura))
{
    Console.WriteLine("Factura guardada exitosamente");
}
else
{
    Console.WriteLine("Error al guardar la factura");
}

//Listar facturas
Console.WriteLine("\nListando todas las facturas");
var todas = f.GetAll();
if (todas.Count == 0)
{
    Console.WriteLine("No hay facturas para mostrar");
}
else
{
    foreach (Factura fac in todas)
    {
        Console.WriteLine(fac.ToString());
    }
}

//Listar factura por ID
Console.WriteLine("\nListando factura por ID");
var facturaporid = f.GetById(1);
if (facturaporid == null)
{
    Console.WriteLine("No hay factura para mostrar");
}
else
{
    Console.WriteLine(facturaporid.ToString());
}



