namespace KleskaProject.Domain.Common.Shared
{
    public class Result
    {
        private readonly Error? _error;

        private Result(bool success, Error? error)
        {
            IsSuccess = success;
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

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;
        public static Result Success() => new(true, null);
        public static Result Failure(Error error) => new(false, error);

        public static implicit operator Result(Error error)
        {
            return Failure(error);
        }
    }

}
