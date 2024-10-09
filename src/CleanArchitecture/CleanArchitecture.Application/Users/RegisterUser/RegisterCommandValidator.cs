using FluentValidation;


namespace CleanArchitecture.Application.Users.RegisterUser;


internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre no puede ser nulo.");
        RuleFor(c => c.Apellidos).NotEmpty().WithMessage("Los Apellidos no pueden ser nulos");
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(5);
    }
}