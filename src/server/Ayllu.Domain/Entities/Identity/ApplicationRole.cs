using Microsoft.AspNetCore.Identity;

namespace Ayllu.Domain.Entities.Identity;

public class ApplicationRole : IdentityRole<string> 
{
    public ApplicationRole()
    {
        Id = Guid.NewGuid().ToString();
        Name = "User";
    }
    public ApplicationRole(string name = "User") : base(name)
    {
        Id = Guid.NewGuid().ToString();
    }

    public virtual ICollection<ApplicationUserRole> Users { get; set; } = [];
}
