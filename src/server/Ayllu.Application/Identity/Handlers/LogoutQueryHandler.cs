using Ayllu.Application.Common.Abstractions.Identity;
using Ayllu.Application.Identity.Queries;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Identity.Handlers;

public sealed class LogoutQueryHandler(IIdentityService @is) : IRequestHandler<LogoutQuery, bool>
{
    public async Task<bool> Handle(IReceiveContext<LogoutQuery> context, CancellationToken cancellationToken)
        => await @is.LogoutAsync(context.Message.UserId, cancellationToken);
}
