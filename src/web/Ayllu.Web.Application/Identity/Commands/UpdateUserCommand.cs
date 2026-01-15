using Ayllu.Web.Application.Identity.Requests;
using Ayllu.Web.Application.Identity.Responses;
using MediatR;

namespace Ayllu.Web.Application.Identity.Commands;

public sealed record UpdateUserCommand(UpdateUserRequest Request, string UserId) : IRequest<ApplicationUserResponse>;
