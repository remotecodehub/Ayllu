using Mediator.Net.Contracts;

namespace Ayllu.Application.Identity.Queries;

public sealed record LogoutQuery(string UserId) : IRequest;
