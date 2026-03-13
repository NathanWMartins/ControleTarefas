using System.Text.Json.Serialization;

namespace TarefasApi.Models
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Message { get; }
        public IEnumerable<string>? Errors { get; }

        protected Result(bool isSuccess, string? message = null, IEnumerable<string>? errors = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Errors = errors;
        }

        public static Result Success(string? message = null) => new(true, message);
        public static Result Failure(string message) => new(false, message);
        public static Result Failure(IEnumerable<string> errors) => new(false, null, errors);

        public static Result<T> Success<T>(T data, string? message = null) => Result<T>.Success(data, message);
        public static Result<T> Failure<T>(string message) => Result<T>.Failure(message);
        public static Result<T> Failure<T>(IEnumerable<string> errors) => Result<T>.Failure(errors);
    }

    public class Result<T> : Result
    {
        public T? Data { get; }

        protected Result(bool isSuccess, T? data = default, string? message = null, IEnumerable<string>? errors = null)
            : base(isSuccess, message, errors)
        {
            Data = data;
        }

        public static Result<T> Success(T data, string? message = null) => new(true, data, message);
        public new static Result<T> Failure(string message) => new(false, default, message);
        public new static Result<T> Failure(IEnumerable<string> errors) => new(false, default, null, errors);
    }
}
