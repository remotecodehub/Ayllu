namespace Ayllu.Web.Application.Identity.Dtos;

public sealed class UserDto
{
    public string Culture { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public IList<string> Roles { get; set; } = [string.Empty];

}
