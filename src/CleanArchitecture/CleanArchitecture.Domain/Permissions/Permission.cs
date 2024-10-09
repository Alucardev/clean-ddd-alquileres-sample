using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Permissions;


public sealed class Permission : Entity<PermissionId>
{

    public Nombre? Nombre { get; init; } 

    private Permission()
    {

    }

    public Permission(PermissionId id, Nombre nombre): base(id)
    {
        Nombre = nombre;
    }

    public Permission(Nombre nombre) : base()
    {
        Nombre = nombre;
    }
    

    public static Result<Permission> Create(Nombre nombre)
    {
        return new Permission(nombre);
    }

}