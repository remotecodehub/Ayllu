using Ayllu.Application.Identity.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Identity.Queries;

public record GetCurrentUserQuery(string UserId) : IRequest<ApplicationUserResponse>;
