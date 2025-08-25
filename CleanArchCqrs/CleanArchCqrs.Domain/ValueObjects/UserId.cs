
namespace CleanArchCqrs.Domain.ValueObjects
{
    public sealed class UserId
    {
        private readonly Guid Value;

        public UserId(Guid guid)
        {
            Value = Guid.NewGuid();
        }

        public static readonly UserId Empty = new(Guid.Empty);

        //public Guid Value { get; private set; }

        public static UserId New() => new(Guid.NewGuid());
        public override string ToString() => Value.ToString();
        public static implicit operator Guid(UserId userId) => userId.Value;
    }
}
