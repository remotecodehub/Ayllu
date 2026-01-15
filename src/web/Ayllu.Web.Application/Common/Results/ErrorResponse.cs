namespace Ayllu.Web.Application.Common.Results;

public sealed record ErrorResponse(int Code, string ErrorCode, string Message, IDictionary<string, string[]>? Errors);