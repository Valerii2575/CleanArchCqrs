
namespace CleanArchCqrs.SharedKernel.Common
{
    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {

        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result<TValue> Success<TValue>(TValue value)  => new(value, true, Error.None);
        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
    }

    public class Result<TValue> : Result
    {
        public readonly TValue? _value;

        protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }
        
        public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("Cannot access Value when Result is a failure.");

        public static implicit operator Result<TValue>(TValue value) => Success(value);

    }
}
