using Ayllu.Application.Common.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Ayllu.Filters;

/// <summary>
/// An endpoint filter that standardizes successful Identity API responses by wrapping them in a result envelope, while
/// preserving special Identity error payloads and other response types.
/// </summary>
/// <remarks>This filter is intended for use with ASP.NET Core Minimal APIs that utilize Identity. It
/// automatically wraps successful 2xx responses with a result envelope, except for responses with no content or special
/// Identity error payloads (such as two-factor authentication requirements), which are returned unchanged. This helps
/// ensure consistent response formatting for clients consuming the API.</remarks>
public sealed class IdentityResultEnvelopeFilter : IEndpointFilter
{
    /// <summary>
    /// Filters the request on identity endpoints
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var result = await next(context);

        // 🔹 Identity usa IResult (Minimal API)
        if (result is not IResult)
            return result;

        // =========================
        // 2XX COM CORPO → encapsula
        // =========================

        if (result is Ok<object> okWithValue)
        {
            return Results.Ok(
                Result<object>.Success(okWithValue.Value!)
            );
        }

        if (result is Ok<string> okString)
        {
            return Results.Ok(
                Result<string>.Success(okString.Value!)
            );
        }

        // 204 / 200 sem body → não encapsula
        if (result is Ok || result is NoContent)
        {
            return result;
        }

        // =========================
        // ERROS GENÉRICOS → mantém
        // =========================

        // ⚠️ NÃO encapsular: payload especial do Identity
        // ex: { "error": "requiresTwoFactor" }
        if (result is UnauthorizedHttpResult ||
            result is ForbidHttpResult)
        {
            return result;
        }

        // Fallback seguro
        return result;
    }
}