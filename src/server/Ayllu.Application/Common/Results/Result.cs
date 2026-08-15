namespace Ayllu.Application.Common.Results;

public sealed class Result<T>
{
    public string? Message { get; }
    public bool IsSuccess { get; }
    public T? Value { get; }
    public ErrorResponse? Error { get; }

    private Result(T value, string message = "")
    {
        IsSuccess = true;
        Value = value;
        Error = null;
        Message = message;
    }

    private Result(ErrorResponse error, string message = "")
    {
        IsSuccess = false;
        Error = error;
        Message = message;
    }

    public static Result<T> Success(T value, string message = "") => new(value, message);
    public static Result<T> Failure(ErrorResponse error, string message = "") => new(error, message);
}