using System.Net;
using CleanArchitecture.Application.Vehiculos.GetVehiculosByPagination;
using CleanArchitecture.Application.Vehiculos.SearchVehiculos;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Permissions;
using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CleanArchitecture.Api.Controllers.Vehiculos;


[ApiController]
[Route("api/vehiculos")]
public class VehiculoController : ControllerBase
{
    private readonly ISender _sender;

    public VehiculoController(ISender sender)
    {
        _sender = sender;
    }
    [HasPermission(PermissionEnum.ReadUser)]
    [HttpGet("search")]
    [ProducesResponseType(typeof(Vehiculo),(int)HttpStatusCode.OK)]
    public async Task<IActionResult> SearchVehiculo(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken
    )
    {
        var query = new SearchVehiculosQuery(startDate, endDate);
        var resultados = await _sender.Send(query, cancellationToken);
        return Ok(resultados.Value);
    }

    [AllowAnonymous]
    [HttpGet("getPagination", Name = "PaginationVehiculo")]
    [ProducesResponseType(typeof(PaginationResult<Vehiculo,VehiculoId>),
        (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationResult<Vehiculo, VehiculoId>>> GetPaginationVehiculo(
        [FromQuery] GetVehiculosByPaginationQuery request 
    )
    {
        var resultados = await _sender.Send(request);
        return Ok(resultados);
    }
}
