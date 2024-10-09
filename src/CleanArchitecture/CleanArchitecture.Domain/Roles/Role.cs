using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Permissions;

namespace CleanArchitecture.Domain.Roles;

public sealed class Role : Enumeration<Role> {

    public Role(int id, string name) : base(id, name)
    {

    }

 

    public ICollection<Permission>? Permissions {get; set;}
    public static readonly Role Cliente = new(1, "Cliente");
    public static readonly Role Admin = new(2, "Admin");


}