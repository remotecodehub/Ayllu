using Ayllu.Application.Identity.Requests;
using Ayllu.Application.Identity.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Identity.Commands;

public sealed record UpdateUserCommand(UpdateUserRequest Request, string UserId) : IRequest<ApplicationUserResponse>;
