using Ayllu.Application.Common.Abstractions.Identity;
using Ayllu.Application.Identity.Queries;
using Ayllu.Application.Identity.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Identity.Handlers;

public sealed class GetCurrentUserQueryHandler(IIdentityService @is) : IRequestHandler<GetCurrentUserQuery, ApplicationUserResponse>
{
    public async Task<ApplicationUserResponse> Handle(IReceiveContext<GetCurrentUserQuery> context, CancellationToken cancellationToken)
        => await @is.GetCurrentUserAsync(context.Message.UserId, cancellationToken);
}
