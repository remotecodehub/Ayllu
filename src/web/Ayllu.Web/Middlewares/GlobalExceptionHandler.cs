using Ayllu.Web.Application.Common.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Ayllu.Web.Middlewares;
/// <summary>
/// Provides extension methods for configuring a global exception handler in an ASP.NET Core application.
/// </summary>
/// <remarks>The global exception handler middleware captures unhandled exceptions during HTTP request processing
/// and returns standardized JSON error responses. This helps ensure consistent error handling and response formatting
/// across the application. To enable global exception handling, call the UseGlobalExceptionHandler extension method
/// during application startup, typically in the Configure method of Startup.cs. The middleware maps common exception
/// types to appropriate HTTP status codes and error messages.</remarks>
public static class GlobalExceptionHandler
{
    /// <summary>
    /// Configures a global exception handler middleware that returns standardized JSON error responses for unhandled
    /// exceptions in the application pipeline.
    /// </summary>
    /// <remarks>This middleware should be registered early in the application's request pipeline to ensure
    /// that unhandled exceptions are consistently caught and formatted. The response will have a content type of
    /// "application/json" and will include an error code and message. Existing response content may be overwritten if
    /// an exception occurs.</remarks>
    /// <param name="app">The application builder used to configure the middleware pipeline. Cannot be null.</param>
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var exceptionFeature =
                    context.Features.Get<IExceptionHandlerFeature>();

                if (exceptionFeature?.Error is null)
                {
                    context.Response.StatusCode =
                        (int)HttpStatusCode.InternalServerError;

                    await context.Response.WriteAsJsonAsync(
                        Result<string>.Failure(new ErrorResponse(500, "UNEXPECTED_ERROR", "An unexpected error occurred.", null))
                    );

                    return;
                }

                var exception = exceptionFeature.Error;

                var (statusCode, errorCode, message) =
                    MapException(exception);

                context.Response.StatusCode = (int)statusCode;

                await context.Response.WriteAsJsonAsync(
                    Result<string>.Failure(new ErrorResponse((int)statusCode, errorCode, message, null)));
            });
        });
    }

    private static (HttpStatusCode Status, string Code, string Message)
        MapException(Exception exception)
    {
        return exception switch
        {
            UnauthorizedAccessException =>
                (HttpStatusCode.Unauthorized, "401", exception.Message),

            AuthenticationFailureException =>
                (HttpStatusCode.Unauthorized, "401", exception.Message),

            BadHttpRequestException =>
                (HttpStatusCode.BadRequest, "400", exception.Message),

            InvalidOperationException =>
                (HttpStatusCode.BadRequest, "400", exception.Message),

            FileNotFoundException =>
                (HttpStatusCode.InternalServerError, "404", exception.Message),

            KeyNotFoundException =>
                (HttpStatusCode.NotFound, "404", exception.Message),

            // aqui você pluga suas exceptions de domínio
            // LicenseInvalidException ex =>
            //     (HttpStatusCode.Forbidden, ex.Code, ex.Message),

            _ =>
                (HttpStatusCode.InternalServerError, "500",
                 "An internal server error occurred.")
        };
    }
}
