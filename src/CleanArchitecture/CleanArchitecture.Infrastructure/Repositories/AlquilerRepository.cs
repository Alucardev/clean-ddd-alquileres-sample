using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class AlquilerRepository : Repository<Alquiler, AlquilerId>, IAlquilerRepository
{
    private static readonly AlquilerStatus[] ActiveAlquilerStatus = {
        AlquilerStatus.Completado,
        AlquilerStatus.Reservado,
        AlquilerStatus.Confirmado
    };
    public AlquilerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<bool> IsOverLappingAsync(Vehiculo vehiculo,
     DateRange duracion, 
     CancellationToken cancellationToken)
    {
        return await DbContext.Set<Alquiler>().AnyAsync(alquiler => alquiler.VehiculoId == vehiculo.Id
        && alquiler.Duracion.Inicio <= duracion.Fin 
        && alquiler.Duracion.Fin >= duracion.Inicio
        && ActiveAlquilerStatus.Contains(alquiler.Status),  cancellationToken);
    }
}