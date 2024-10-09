using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos;

public record SearchVehiculosQuery(
    DateOnly fechaInicio,
    DateOnly fechaFin
) : IQuery<IReadOnlyList<VehiculoResponse>>;