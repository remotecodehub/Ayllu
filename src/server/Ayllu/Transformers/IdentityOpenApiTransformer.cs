using Ayllu.Utils;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Ayllu.Transformers;

/// <summary>
/// OpenApiTransformer class for Identity endpoints
/// </summary>
public sealed class IdentityOpenApiTransformer : IOpenApiOperationTransformer
{
    /// <summary>
    /// Transforms the specified OpenAPI operation by updating its summary and description for recognized authentication
    /// endpoints.
    /// </summary>
    /// <remarks>If the operation corresponds to a standard authentication endpoint (such as login, register,
    /// or refresh), its summary and description are set to indicate that it is an automatic ASP.NET Identity endpoint.
    /// No changes are made for other endpoints.</remarks>
    /// <param name="operation">The OpenAPI operation to be transformed. This object may be modified to update its summary and description.</param>
    /// <param name="context">The context containing metadata and information about the current operation being transformed.</param>
    /// <param name="cancellationToken">A token that can be used to request cancellation of the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous transformation operation.</returns>
    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        var path = context.Description.RelativePath!.Replace($"api/{Constants.ApiVersion}/identity/", "");
        var method = context.Description.HttpMethod;

        if (path is null)
            return Task.CompletedTask;

        if (path.Equals("login", StringComparison.OrdinalIgnoreCase))
        {
            operation.Summary = "Login do usuário";
            operation.Description = "Autentica o usuário e retorna tokens de acesso.";
        }
        else if (path.Equals("register", StringComparison.OrdinalIgnoreCase))
        {
            operation.Summary = "Registro de usuário";
            operation.Description = "Cria uma nova conta de usuário.";
        }
        else if (path.Equals("refresh", StringComparison.OrdinalIgnoreCase))
        {
            operation.Summary = "Refresh de token";
            operation.Description = "Renova o token de acesso utilizando um refresh token válido.";
        }
        else if (path.Equals("confirmEmail", StringComparison.OrdinalIgnoreCase))
        {
            operation.Summary = "Confirmar e-mail";
            operation.Description = "Confirma o endereço de e-mail do usuário.";
        }
        else if (path.Equals("resendConfirmationEmail", StringComparison.OrdinalIgnoreCase))
        {
            operation.Summary = "Tentar confirmar e-mail novamente";
            operation.Description = "Realiza uma nova tentativa de confirmar o endereço de e-mail do usuário.";
        }
        else if (path.Equals("forgotPassword", StringComparison.OrdinalIgnoreCase))
        {
            operation.Summary = "Lembrar senha";
            operation.Description = "Envia um link de redefinição de senha para o email do usuário.";
        }
        else if (path.Equals("resetPassword", StringComparison.OrdinalIgnoreCase))
        {
            operation.Summary = "Troca de senha";
            operation.Description = "Solicita a senha atual e uma nova senha para substituir a senha atual do usuário.";
        }
        else if (path.Equals("manage/2fa", StringComparison.OrdinalIgnoreCase))
        {
            operation.Summary = "Segundo fator de autenticação";
            operation.Description = "Gerencia segundo fator de autenticação do usuário.";
        }
        else if (path.Equals("manage/info", StringComparison.OrdinalIgnoreCase))
        {
            if (method!.Equals("get", StringComparison.OrdinalIgnoreCase))
            {
                operation.Summary = "Informações";
                operation.Description = "Obtem informações do email do usuário.";
            }
            else if (method!.Equals("post", StringComparison.OrdinalIgnoreCase))
            {
                operation.Summary = "Atualizar email ";
                operation.Description = "Atualiza o email do usuário solicitando uma senha nova e a senha atual junto ao novo email.";
            }
        } 

        return Task.CompletedTask;
    }
}