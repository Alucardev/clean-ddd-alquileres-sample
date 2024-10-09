using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Domain.Alquileres;


public sealed class Alquiler : Entity<AlquilerId>
{

    private Alquiler()
    {
        
    }
    private Alquiler(
        
        AlquilerId id,
        VehiculoId vehiculoid,
        UserId userId,
        DateRange duracion,
        Moneda precioPorPeriodo,
        Moneda mantenimiento,
        Moneda accesorios,
        Moneda precioTotal,
        AlquilerStatus status,
        DateTime fechaCreacion
    
    ) : base(id)  {

        VehiculoId = vehiculoid;
        UserId = userId;
        Duracion = duracion;
        PrecioPorPeriodo = precioPorPeriodo;
        Mantenimiento = mantenimiento;
        Accesorios = accesorios;
        PrecioTotal = precioTotal;
        Status = status;
        FechaCreacion = fechaCreacion;
    }

    public VehiculoId VehiculoId { get; private set; }

    public UserId UserId { get; private set; }
    public Moneda? PrecioPorPeriodo {get; private set;}
    public Moneda? Mantenimiento {get; private set;}

    public Moneda? Accesorios {get; private set;}

    public Moneda? PrecioTotal {get; private set;}  

    public AlquilerStatus Status {get; private set;}
    
    public DateRange? Duracion {get; private set;}

    public DateTime? FechaCreacion {get; private set;}  

    public DateTime? FechaConfirmacion {get; private set;}

    public DateTime? FechaDenegacion {get; private set;}

    public DateTime? FechaCompletado {get; private set;}

    public DateTime? FechaCancelacion {get; private set;}


    public static Alquiler Reservar(
        Vehiculo vehiculo,
        UserId userId,
        DateRange duracion,
        DateTime fechaCreacion,
        PrecioService precioService
    )
    {

        var precioDetalle = precioService.CalcularPrecio(
            vehiculo, duracion
        );

        var alquiler  = new Alquiler(
            AlquilerId.New(),
            vehiculo.Id,
            userId,
            duracion,
            precioDetalle.PrecioPorperiodo,
            precioDetalle.Mantenimiento,
            precioDetalle.Accesorios,
            precioDetalle.PrecioTotal,
            AlquilerStatus.Reservado,
            fechaCreacion
        );


        alquiler.RaiseDomainEvent(new AlquilerReservadoDomainEvent(alquiler.Id!));
        
        vehiculo.FechaUltimaAlquiler = fechaCreacion;
        return alquiler;
    }


      public Result Confirmar(DateTime UtcNow){
        if(Status != AlquilerStatus.Reservado){
            Result.Failure(AlquilerErrors.NotReserved);
        }

        Status = AlquilerStatus.Confirmado;
        FechaConfirmacion = UtcNow;

        RaiseDomainEvent(new AlquilerReservadoDomainEvent(Id));
        return Result.Success();
    }


    public Result Rechazar(DateTime UtcNow){
        if(Status != AlquilerStatus.Reservado){
            return  Result.Failure(AlquilerErrors.NotReserved);
        }

        Status = AlquilerStatus.Rechazado;
        FechaDenegacion = UtcNow;
        RaiseDomainEvent(new AlquilerRechazadoDomainEvent(Id));

        return Result.Success(true);
    }

       public Result Cancelar(DateTime UtcNow){
        if(Status != AlquilerStatus.Confirmado){
            return  Result.Failure(AlquilerErrors.NotConfirmado);
        }

        var currentDate = DateOnly.FromDateTime(UtcNow);
        if(currentDate > Duracion!.Inicio){
            return Result.Failure(AlquilerErrors.AlreadyStarted);
        }

        Status = AlquilerStatus.Cancelado;
        FechaCancelacion = UtcNow;
        RaiseDomainEvent(new AlquilerCanceladoDomainEvent(Id));

        return Result.Success(true);
    }


          public Result Completar(DateTime UtcNow){
        if(Status != AlquilerStatus.Confirmado){
            Result.Failure(AlquilerErrors.NotConfirmado);
        }

        Status = AlquilerStatus.Completado;
        FechaCompletado= UtcNow;
        RaiseDomainEvent(new AlquilerCompletadoDomainEvent(Id));
        
        return Result.Success();
    }
}