namespace Ayllu.Application.Identity.Responses;

public sealed class ApplicationUserResponse

{
    public ApplicationUserResponse()
    {
        Culture = "pt-BR";
        Email = string.Empty;
        FullName = string.Empty;
        Id = string.Empty;
        PhoneNumber = string.Empty;
        Roles = [];
        UserName = string.Empty;
    }
    public ApplicationUserResponse(string culture, string email, string firstName, string surname, string id, string phoneNumber, IList<string> roles, string userName)
    {
        Culture = culture;
        Email = email;
        FullName = $"{firstName} {surname}";
        Id = id;
        PhoneNumber = phoneNumber;
        Roles = roles;
        UserName = userName;
    }
    public string Culture { get; internal set; } 
    public string Email { get; internal set; } 
    public string FullName { get; internal set; } 
    public string Id { get; internal set; } 
    public string PhoneNumber { get; internal set; } 
    public IList<string> Roles { get; internal set; }
    public string UserName { get; internal set; } 
}
