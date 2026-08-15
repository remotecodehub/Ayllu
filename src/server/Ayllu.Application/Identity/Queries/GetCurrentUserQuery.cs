using Ayllu.Application.Identity.Responses;
using MediatR;

namespace Ayllu.Application.Identity.Queries;

public record GetCurrentUserQuery(string UserId) : IRequest<ApplicationUserResponse>;

