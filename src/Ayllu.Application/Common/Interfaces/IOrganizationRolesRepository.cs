namespace Ayllu.Application.Common.Interfaces;

public interface IOrganizationRolesRepository
{
    Task<IList<string>> GetUserOrganizationRolesAsync(string userId);
    Task<bool> IsUserInRoleInOrganizationAsync(string userId, string organizationId, string roleName);
}
