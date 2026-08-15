namespace Ayllu.Application.Identity.Commands;

public sealed record UpdateUserCommand(UpdateUserRequest Request, string UserId) : IRequest;
