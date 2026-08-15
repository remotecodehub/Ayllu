using Ayllu.Application.Common.Abstractions.Identity;
using Ayllu.Application.Identity.Queries;
using MediatR;

namespace Ayllu.Application.Identity.Handlers;

public sealed class LogoutQueryHandler(IIdentityService @is) : IRequestHandler<LogoutQuery, bool>
{
    public async Task<bool> Handle(LogoutQuery request, CancellationToken cancellationToken)
        => await @is.LogoutAsync(request.UserId, cancellationToken);
}
