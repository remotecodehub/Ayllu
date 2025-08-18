using Ayllu.Application.Common.Interfaces;
using Ayllu.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ayllu.Application.Services;

public class IdentityService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenGenerator tokenGenerator,
        IOrganizationRolesRepository context) : IIdentityService
{
    public async Task<(bool Success, string? Token, IEnumerable<string>? Errors)> ForgotPasswordAsync(string email)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return (false, null, new[] { "User not found" });

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            // Aqui você enviaria o email via IEmailSender (não implementado ainda)
            // await _emailSender.SendAsync(user.Email, "Password Reset", $"Token: {token}");

            return (true, token, null);
        }
        catch (Exception e)
        {

            throw;
        }
    }

    public async Task<(bool Success, string Jwt, DateTime Expiration, string RefreshToken, string? Email, string? PhoneNumber, string? UserName, IEnumerable<string> Roles, IEnumerable<string>? Errors)> LoginAsync(string key, string password)
    {
        try
        {
            var user = userManager.Users.FirstOrDefault(u => u.UserName == key || u.Email == key || u.PhoneNumber == key);
            if (user == null)
                return (false, string.Empty, DateTime.MinValue, string.Empty, string.Empty, string.Empty, string.Empty, [], new[] { "Invalid credentials" });

            var result = await signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                return (false, string.Empty, DateTime.MinValue, string.Empty, string.Empty, string.Empty, string.Empty, [], new[] { "Invalid credentials" });

            var roles = await userManager.GetRolesAsync(user);

            // Pegar roles de organização (se houver, simplificado)
            var orgRoles = await context.GetUserOrganizationRolesAsync(user.Id);

            var (jwt, expiration, refreshToken) = tokenGenerator.GenerateToken(
                user.UserName!,
                user.Email!,
                orgRoles.Any() ? "MemberOfOrganization" : "Independent",
                user.PhoneNumber ?? string.Empty,
                roles,
                orgRoles
            );

            return (true, jwt, expiration, refreshToken, user.Email, user.PhoneNumber, user.UserName, roles, null);
        }
        catch (Exception e)
        {

            throw;
        }
    }

    public async Task LogoutAsync(string jwt)
    {
        try
        {

            tokenGenerator.Invalidate(jwt);

            await Task.CompletedTask;
        }
        catch (Exception e)
        {

            throw;
        }
    }

    public async Task<(bool Success, string? UserId, int StatusCode, IEnumerable<string>? Errors)> RegisterAsync(string username, string email, string password, string name, string lastName, string phoneNumber)
    {
        try
        {
            var user = new AppUser
            {
                UserName = username,
                Email = email,
                Name = name,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser is not null)
                return (false, null, 409, new[] { "Email already in use" });

            existingUser = await userManager.FindByNameAsync(username);

            if (existingUser is not null)
                return (false, null, 409, new[] { "Username already in use" });

            existingUser = userManager.Users.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefault();

            if (existingUser is not null)
                return (false, null, 409, new[] { "Phone number already in use" });
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return (false, null, 400, result.Errors.Select(e => e.Description));

            return (true, user.Id, 202, null);
        }
        catch (Exception e)
        {

            throw;
        }
    }

    public async Task<(bool Success, IEnumerable<string>? Errors)> ResetPasswordAsync(string email, string token, string newPassword)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return (false, new[] { "User not found" });

            var result = await userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            return (true, null);
        }
        catch (Exception e)
        {

            throw;
        }
    }
}
