namespace Ayllu.Domain.Entities
{

    public class AppUserOrganization
    {
        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; } = null!;

        public string OrganizationId { get; set; } = string.Empty;
        public Organization Organization { get; set; } = null!;

        public string Role { get; set; } = OrganizationRole.Member.ToString();
    }
}