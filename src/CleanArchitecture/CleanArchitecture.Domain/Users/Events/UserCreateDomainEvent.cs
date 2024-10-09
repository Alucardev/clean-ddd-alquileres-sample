using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users.Events;


public sealed class UserCreatedDomainEvent(UserId UserId): IDomainEvent;