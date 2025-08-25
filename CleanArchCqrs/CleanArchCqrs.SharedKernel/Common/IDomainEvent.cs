
namespace CleanArchCqrs.SharedKernel.Common
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        DateTimeOffset OccurredOn { get; }
    }

    public abstract record DomainEvent(Guid Id, DateTimeOffset OccurredOn) : IDomainEvent
    {
        
        protected DomainEvent() : this(Guid.NewGuid(), DateTimeOffset.UtcNow)
        {
        }        
    }
}
