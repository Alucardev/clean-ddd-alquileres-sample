

using CleanArchitecture.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Middleware;

public class ExceptionHandlingMiddleware
{

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context){
        try{
            await _next(context);
        }
        catch(Exception exception){
            _logger.LogError(exception, "Ocurrio una excepcion:", exception);
            var excepcionDetails = GetExceptionDetails(exception);
            var problemDetails = new ProblemDetails
            {
                Status = excepcionDetails.Status,
                Type = excepcionDetails.Type,
                Title = excepcionDetails.Title,
                Detail = excepcionDetails.Detail
            };

            if(excepcionDetails.Errors is not null)
            {
                problemDetails.Extensions["errors"] = excepcionDetails.Status;
            }

            context.Response.StatusCode = excepcionDetails.Status;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception exception){

        return exception switch
        {
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "Validacion de Error",
                "han ocurrido uno o mas errores de validacion",
                validationException.Errors
                ),

                _ => new ExceptionDetails(
                    StatusCodes.Status500InternalServerError,
                    "ServerError",
                    "Error de Servidor",
                    "Un inesperado error a ocurrida en la App",
                    null
                )
        };
    }

    internal record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<Object>? Errors
    );

    

}