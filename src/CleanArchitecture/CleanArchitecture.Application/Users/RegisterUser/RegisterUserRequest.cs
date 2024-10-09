namespace CleanArchitecture.Application.Users.RegisterUser;


public record RegisterUserRequest
(
    string email,
    string nombre,
    string Apellidos,
    string Password
);