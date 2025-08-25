using CleanArchCqrs.SharedKernel.Common;

namespace CleanArchCqrs.Domain.Events
{
    public sealed record UserCreateDomainEvents(Guid UserId, string Email, string FullName) : DomainEvent { }
    public sealed record UserNameUpdateDomainEvent(Guid UserId, string NewFullName) : DomainEvent { }

    public sealed record UserDeactivatedDomainEvent(Guid UserId) : DomainEvent { }
}
