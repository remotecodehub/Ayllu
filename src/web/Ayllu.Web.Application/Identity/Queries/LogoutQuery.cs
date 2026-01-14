using MediatR;

namespace Ayllu.Web.Application.Identity.Queries;

public sealed record LogoutQuery(string UserId) : IRequest<bool>;
