namespace Ayllu.Application.Identity.Requests;

public sealed record UpdateUserRequest(string UserName, string FirstName, string Surname, string PhoneNumber, string Culture);
