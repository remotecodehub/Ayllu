
using Ayllu.Web.Application.Identity.Responses;
using MediatR;

namespace Ayllu.Web.Application.Identity.Queries;

public record GetCurrentUserQuery(string UserId) : IRequest<ApplicationUserResponse>;

