namespace CleanArchitecture.Api.Controllers.Alquiler;


public sealed record AlquilerReservaRequest (
    Guid VehiculoId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
);