using Ejercicio1_5.Domain;

namespace Ejercicio1_5.Data.Interface
{
    public interface IDetalleRepository
    {
        List<Detalle> GetDetalles(int facturaId);
    }
}