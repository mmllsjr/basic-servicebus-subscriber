namespace BasicTopicSubscriber.Core.DTOs
{
    public sealed class Result<T>
    {
        public T Value { get; private set; }
        public string ErrorMessage { get; private set; }

        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);

        public static Result<T> Failure(string errorMessage)
        {
            return new()
            {
                ErrorMessage = errorMessage
            };
        }

        public static Result<T> Success(T value)
        {
            return new()
            {
                Value = value
            };
        }
    }
}
