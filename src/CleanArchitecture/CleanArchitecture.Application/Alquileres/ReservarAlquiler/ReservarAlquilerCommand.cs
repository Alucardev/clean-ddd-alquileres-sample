using CleanArchitecture.Application.Abstractions.Messaging;

public record ReservarAlquilerCommand(

    Guid VehiculoId,
    Guid UserId,
    DateOnly FechaInicio,
    DateOnly FechaFin

): ICommand<Guid>;