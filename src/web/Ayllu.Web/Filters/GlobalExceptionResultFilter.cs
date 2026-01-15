using Ayllu.Web.Application.Common.Exceptions;
using Ayllu.Web.Application.Common.Results;
using Ayllu.Web.Domain.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Security.Authentication;

namespace Ayllu.Web.Filters;

/// <summary>
/// Global filter for handles errors and exceptions results in all endpoints
/// </summary>
public sealed class GlobalExceptionResultFilter :
    IAsyncExceptionFilter,
    IAsyncResultFilter
{
    /* =========================
     *  EXCEPTIONS
     * ========================= */
    /// <summary>
    /// Handles the exceptions thrown in the application to standardize error responses.
    /// </summary>
    /// <param name="context">The context of exception thrown</param>
    /// <returns>The task completed</returns>
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;

        var (statusCode, errorCode, message) = exception switch
        {
            DomainException ex => (
                HttpStatusCode.BadRequest,
                "DOMAIN",
                ex.Message
            ),
            UnauthorizedAccessException => (
                HttpStatusCode.Unauthorized,
                "UNAUTHORIZED",
                "Authentication is required to access this resource."
            ),
            AuthenticationException => (
                HttpStatusCode.Unauthorized,
                "UNAUTHENTICATED",
                "Invalid authentication credentials."
            ),
            KeyNotFoundException ex => (
                HttpStatusCode.NotFound,
                "NOT_FOUND",
                ex.Message
            ),
            EntityNotFoundException ex => (
                HttpStatusCode.NotFound,
                "NOT_FOUND",
                ex.Message
            ),
            EntityConflictException ex => (
                HttpStatusCode.Conflict,
                "CONFLICT",
                ex.Message
            ),
            ApplicationValidationException ex => (
                (HttpStatusCode)ParseStatusCode(ex.ErrorCode),
                "VALIDATION",
                ex.Message
            ),
            OperationCanceledException oce => (
                HttpStatusCode.RequestTimeout,
                "CANCELLED",
                oce.Message
            ),
            TimeoutException te => (
                HttpStatusCode.RequestTimeout,
                "TIMEOUT",
                te.Message
            ),
            _ => (
                HttpStatusCode.InternalServerError,
                "INTERNAL",
                "An unexpected server error occurred."
            )
        };
        
        
        var errors = new Dictionary<string, string[]>();
        errors.Add($"Exception {errorCode}", [exception.Message]);
        
        var response = Result<string>.Failure(new ErrorResponse((int)statusCode, errorCode, message, errors));
        context.Result = new ObjectResult(response)
        {
            StatusCode = (int)statusCode
        };

        context.ExceptionHandled = true;
        return Task.CompletedTask;
    }

    /* =========================
     *  HTTP RESULT STATUS CODES
     * ========================= */
    /// <summary>
    /// Handles the execution of results to standardize error responses based on HTTP status codes.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public Task OnResultExecutionAsync(
        ResultExecutingContext context,
        ResultExecutionDelegate next)
    {
        if (context.Result is ForbidResult)
        {
            context.Result = new ObjectResult(
                Result<string>.Failure(new ErrorResponse(403, "FORBIDDEN", "You do not have permission to access this resource.", null)))
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

            return Task.CompletedTask;
        }

        if (context.Result is UnauthorizedObjectResult)
        {
            context.Result = new ObjectResult(Result<string>.Failure(new ErrorResponse(401, "AUTHORIZATION", "Authorization required", null)))
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            return Task.CompletedTask;
        }

        if (context.Result is UnauthorizedResult)
        {
            context.Result = new ObjectResult(Result<string>.Failure(new ErrorResponse(401, "AUTHORIZATION", "Authorization required", null)))
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
            return Task.CompletedTask;
        }

        if (context.Result is not ObjectResult objectResult)
        {
            return next();
        }
        var statusCode = objectResult.StatusCode ?? StatusCodes.Status200OK;

        if (statusCode >= 400)
        {
            var (message, code) = statusCode switch
            {
                StatusCodes.Status401Unauthorized =>
                      ("Requer autenticação (login e senha).",
                      "UNAUTHORIZED"),
                StatusCodes.Status403Forbidden =>
                    ("O cliente não tem permissão para acessar o conteúdo (mesmo autenticado).",
                    "FORBIDDEN"),
                StatusCodes.Status404NotFound =>
                    ("Recurso Nao Encontrado",
                    "NOT_FOUND"),
                StatusCodes.Status405MethodNotAllowed =>
                    ("Método não permitido",
                    "NOT_ALOWED"),
                StatusCodes.Status406NotAcceptable =>
                    ("O servidor não pode gerar uma resposta que atenda aos critérios do cliente.",
                    "NOT_ACCEPTABLE"),
                StatusCodes.Status409Conflict =>
                    ("Operação em conflito",
                    "CONFLICT"),
                StatusCodes.Status422UnprocessableEntity =>
                    ("Validation failed.",
                    "VALIDATION"),
                StatusCodes.Status500InternalServerError =>
                    ("An internal server error occurred.",
                    "INTERNAL"),
                _ =>
                    ("An error occurred while processing the request.",
                    "INTERNAL")
           }
        ;

            objectResult.Value = Result<string>.Failure(new ErrorResponse((int)statusCode, code, message, null));

        }
        ;

        return next();
    }

    private static int ParseStatusCode(string errorCode)
    {
        return int.TryParse(errorCode, out var status)
            && Enum.IsDefined(typeof(HttpStatusCode), status)
                ? status
                : StatusCodes.Status422UnprocessableEntity;
    }

}