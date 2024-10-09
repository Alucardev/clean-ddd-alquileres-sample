using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Alquileres;

public record PrecioDetalle(
    Moneda PrecioPorperiodo,
    Moneda Mantenimiento,
    Moneda Accesorios,
    Moneda PrecioTotal
);