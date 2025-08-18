using Ayllu.Application.DTO.Responses;
using Ayllu.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore; 
using System.Text.Json; 

namespace Ayllu.Infrastructure.Common.Middlewares;

public class InvalidatedTokenMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();

            // Validação rápida: se está no banco, bloqueia
            var tokenExists = await dbContext.InvalidatedTokens
                .AsNoTracking()
                .AnyAsync(t => t.Jwt == token);

            if (tokenExists)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(
                    new Response
                    {
                        Success = false,
                        Message = "Token has been invalidated.",
                        StatusCode = StatusCodes.Status401Unauthorized
                    })
                );
                return;
            }
        }

        // Continua o pipeline
        await next(context);
    }
}
