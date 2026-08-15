namespace Ayllu.Application.Identity.Queries;

public record GetCurrentUserQuery(string UserId) : IRequest;
