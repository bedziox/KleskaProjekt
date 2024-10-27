namespace KleskaProject.Domain.Common.Shared
{
    public class Result<T>
    {
        private readonly Error? _error;

        private readonly T? _value;

        private Result(bool success, T? value, Error? error)
        {
            IsSuccess = success;
            _value = value;
            _error = error;
        }

        public Error Error
        {
            get
            {
                if (IsSuccess)
                {
                    throw new InvalidOperationException("Cannot get error from successful result.");
                }

                return _error!;
            }
        }

        public T? Value
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException("Cannot get value from failed result.");
                }

                return _value;
            }
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public static Result<T> Success(T value) => new(true, value, null);

        public static Result<T> Failure(Error error) => new(false, default, error);

        public static implicit operator Result<T>(Error error)
        {
            ArgumentNullException.ThrowIfNull(error);
            return Failure(error);
        }
    }

}
