
namespace CleanArchCqrs.SharedKernel.Common
{
    public abstract class BaseEntity<TId> : IEquatable<BaseEntity<TId>>
        where TId : notnull
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected BaseEntity(TId id)
        {
            Id = id;
        }

        public TId Id { get; private set; }
        public DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; protected set; } = DateTimeOffset.UtcNow;
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void MarkAsUpdated()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public bool Equals(BaseEntity<TId> other)
        {
            return other is not null && Id.Equals(other.Id);
        }

        public override bool Equals(object? obj)
        {
            return obj is BaseEntity<TId> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity<TId>? left, BaseEntity<TId>? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseEntity<TId>? left, BaseEntity<TId>? right)
        {
            return !Equals(left, right);
        }
    }
}
