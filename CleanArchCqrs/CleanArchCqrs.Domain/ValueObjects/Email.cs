using CleanArchCqrs.SharedKernel.Common;

namespace CleanArchCqrs.Domain.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; }
        private Email(string value)
        {
            Value = value;
        }
        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure<Email>(new Error("Email.Empty", "Email cannot be empty."));
            }
            // Simple email validation
            if (!IsValidEmail(email))
            {
                return Result.Failure<Email>(new Error("Email.InvalidFormat", "Email format is invalid."));
            }
            return Result.Success(new Email(email));
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public override string ToString() => Value;
        public static implicit operator string(Email email) => email.Value;
    }
}
