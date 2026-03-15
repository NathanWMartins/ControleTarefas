using System.Text.Json.Serialization;

namespace TarefasApi.Models
{
    public enum ErrorType
    {
        None,
        Failure,
        NotFound,
        Unauthorized,
        Conflict,
        ValidationError
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public string? Message { get; }
        public IEnumerable<string>? Errors { get; }
        public ErrorType ErrorType { get; }

        protected Result(bool isSuccess, string? message = null, IEnumerable<string>? errors = null, ErrorType errorType = ErrorType.None)
        {
            IsSuccess = isSuccess;
            Message = message;
            Errors = errors;
            ErrorType = errorType;
        }

        public static Result Success(string? message = null) => new(true, message);
        public static Result Failure(string message, ErrorType errorType = ErrorType.Failure) => new(false, message, null, errorType);
        public static Result Failure(IEnumerable<string> errors, ErrorType errorType = ErrorType.ValidationError) => new(false, null, errors, errorType);

        public static Result<T> Success<T>(T data, string? message = null) => Result<T>.Success(data, message);
        public static Result<T> Failure<T>(string message, ErrorType errorType = ErrorType.Failure) => Result<T>.Failure(message, errorType);
        public static Result<T> Failure<T>(IEnumerable<string> errors, ErrorType errorType = ErrorType.ValidationError) => Result<T>.Failure(errors, errorType);
    }

    public class Result<T> : Result
    {
        public T? Data { get; }

        protected Result(bool isSuccess, T? data = default, string? message = null, IEnumerable<string>? errors = null, ErrorType errorType = ErrorType.None)
            : base(isSuccess, message, errors, errorType)
        {
            Data = data;
        }

        public static Result<T> Success(T data, string? message = null) => new(true, data, message);
        public new static Result<T> Failure(string message, ErrorType errorType = ErrorType.Failure) => new(false, default, message, null, errorType);
        public new static Result<T> Failure(IEnumerable<string> errors, ErrorType errorType = ErrorType.ValidationError) => new(false, default, null, errors, errorType);
    }
}
