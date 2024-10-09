using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.LoginUser;

public record LoginCommand(string Email, string Password) :  ICommand<string>;

