namespace Ayllu.Application.Common.Exceptions;

public sealed class ApplicationValidationException(string errorCode, string message, Exception? inner = null!) : Exception(message, inner)
{
    public string ErrorCode { get; } = errorCode;
}