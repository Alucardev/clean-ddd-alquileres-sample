using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Users.RegisterUser;


public sealed record RegisterUserCommand(
    string Email,
     string Nombre, 
     string Apellidos, 
     string Password
): ICommand<Guid>;