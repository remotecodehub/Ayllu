using Ayllu.Application.Common.Abstractions.Identity;
using Ayllu.Application.Identity.Requests;
using Ayllu.Application.Identity.Responses;
using Ayllu.Domain.Entities.Identity;
using Ayllu.Application.Identity.Dtos;
using Ayllu.Domain.Exceptions.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ayllu.Infrastructure.Common.Services.Identity;

public class IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IApplicationUserFriendshipRepository userFriendRepository, ILogger<IdentityService> logger) : IIdentityService
{
    public async Task<ApplicationUserResponse> GetCurrentUserAsync(string userId, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"{nameof(GetCurrentUserAsync)} starts");
            var user = await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Usuário não encontrado");
            var roles = await userManager.GetRolesAsync(user);
            return new ApplicationUserResponse(
                user.Culture ?? string.Empty, 
                user.Email ?? string.Empty, 
                user.Name ?? string.Empty, 
                user.LastName ?? string.Empty, 
                userId, 
                user.PhoneNumber ?? string.Empty, 
                roles, 
                user.UserName ?? string.Empty);
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetCurrentUserAsync has errors: {Message} , {StackTrace}", e.Message, e.StackTrace);
            throw;
        }
        finally
        {
            logger.LogInformation($"{nameof(GetCurrentUserAsync)} finishes");
        }
    }

    public async Task<ICollection<ApplicationUserFriendResponse>> GetUserFriendsAsync(string userId, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"{nameof(GetUserFriendsAsync)} starts");
            var userWithFriends = await userManager.Users
                .AsQueryable()
                .AsNoTracking()
                .Include(u => u.FriendshipsInitiated!)
                    .ThenInclude(f => f.UserB)
                .Include(u => u.FriendshipsReceived!)
                    .ThenInclude(f => f.UserA)
                .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken) ?? throw new EntityNotFoundException("Usuario nao encontrado");
            return [.. userWithFriends.Friends.Select(x => new ApplicationUserFriendResponse(userId, x.Id))];
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetUserFriendsAsync has errors: {Message}", e.Message);
            throw;
        }
        finally
        {
            logger.LogInformation($"{nameof(GetUserFriendsAsync)} finishes");
        }
    }

    public async Task<bool> LogoutAsync(string userId, CancellationToken cancellation)
    {
        try
        {
            logger.LogInformation($"{nameof(LogoutAsync)} starts");
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Atualiza o Security Stamp, invalidando tokens atuais
            var result = await userManager.UpdateSecurityStampAsync(user);

            // Se estiver usando Cookies em paralelo, limpa a sessão local
            await signInManager.SignOutAsync();

            return result.Succeeded;
        }
        catch (Exception e)
        {
            logger.LogError(e, "LogoutAsync has errors: {Message}", e.Message);
            throw;
        }
        finally
        {
            logger.LogInformation($"{nameof(LogoutAsync)} finishes");
        }
    }

    public async Task<ApplicationUserFriendResponse> SendInviteUserFriendAsync(string userId, string friendId, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"{nameof(SendInviteUserFriendAsync)} starts");
            return new ApplicationUserFriendResponse(userId, friendId, "");
        }
        catch (Exception e)
        {
            logger.LogError(e, "SendInviteUserFriendAsync has errors: {Message}", e.Message);
            throw;
        }
        finally
        {
            logger.LogInformation($"{nameof(SendInviteUserFriendAsync)} finishes");
        }
    }

    public async Task<ApplicationUserResponse> UpdateUserAsync(UpdateUserRequest request, string userId, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"{nameof(UpdateUserAsync)} starts");
            var user = await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Usuário não encontrado");
            var roles = await userManager.GetRolesAsync(user);
            user.UserName = request.UserName;
            user.NormalizedUserName = request.UserName.ToUpperInvariant();
            user.PhoneNumber = request.PhoneNumber;
            user.PhoneNumberConfirmed = false;
            user.Culture = request.Culture;
            user.Name = request.FirstName;
            user.LastName = request.Surname;
            var result = await userManager.UpdateAsync(user);
            return result.Succeeded ? new ApplicationUserResponse(user.Culture, user.Email ?? string.Empty, user.Name, user.LastName, userId, user.PhoneNumber, roles, user.UserName)
            : throw new InvalidOperationException($"Ocorreram erros ao atualizar o perfi: {string.Join(", ", result.Errors.Select(e => e.Description).ToList())}");
        }
        catch (Exception e)
        {
            logger.LogError(e, "GetCurrentUserAsync has errors: {Message} , {StackTrace}", e.Message, e.StackTrace);
            throw;
        }
        finally
        {
            logger.LogInformation($"{nameof(UpdateUserAsync)} finishes");
        }
    }

}
