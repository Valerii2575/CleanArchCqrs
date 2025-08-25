using CleanArchCqrs.Domain.Events;
using CleanArchCqrs.Domain.ValueObjects;
using CleanArchCqrs.SharedKernel.Common;

namespace CleanArchCqrs.Domain.Entities
{
    public sealed class User : BaseEntity<UserId>
    {
        private User() : base(UserId.Empty) { } // For EF Core

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public bool IsActive { get; private set; } = true;

        private User(UserId id, string firstName, string lastName, Email email) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;

            RaiseDomainEvent(new UserCreateDomainEvents(id, email.Value, $"{firstName} {lastName}"));
        }

        public static Result<User> Create(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result.Failure<User>(new Error("User.FirstName.Empty", "First name cannot be empty."));
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result.Failure<User>(new Error("User.LastName.Empty", "Last name cannot be empty."));
            }

            var emailResult = Email.Create(email);
            if (emailResult.IsFailure)
            {
                return Result.Failure<User>(emailResult.Error);
            }

            var userId = UserId.New();
            var user = new User(userId, firstName, lastName, emailResult.Value);
            // You can raise a domain event here if needed
            // user.RaiseDomainEvent(new UserCreatedEvent(user));
            return Result.Success(user);
        }

        public Result UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result.Failure(new Error("User.FirstName.Empty", "First name cannot be empty."));
            }
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result.Failure(new Error("User.LastName.Empty", "Last name cannot be empty."));
            }
            FirstName = firstName;
            LastName = lastName;

            MarkAsUpdated();
            RaiseDomainEvent(new UserNameUpdateDomainEvent(Id, $"{firstName} {lastName}"));
            return Result.Success();
        }

        public void Deactivate()
        {
            if (!IsActive)
                return;

            IsActive = false;
            MarkAsUpdated();
            RaiseDomainEvent(new UserDeactivatedDomainEvent(Id));
        }
    }
}
