using Ayllu.Application.Common.Interfaces;
using Ayllu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore; 

namespace Ayllu.Infrastructure.Repositories 
{
    public class OrganizationRolesRepository(AppDbContext context) : IOrganizationRolesRepository
    {
        public async Task<IList<string>> GetUserOrganizationRolesAsync(string userId)
        {
            return await context.UserOrganizations
                .Include(uo => uo.Organization)
                .Where(uo => uo.UserId == userId)
                .Select(uo => uo.Role)
                .ToListAsync();
        }

        public async Task<bool> IsUserInRoleInOrganizationAsync(string userId, string organizationId, string roleName)
        {
            return await context.UserOrganizations
                .Include(uo => uo.Organization)
                .AnyAsync(uo =>
                    uo.UserId == userId &&
                    uo.OrganizationId == organizationId &&
                    uo.Role == roleName);
        }
    }
}
