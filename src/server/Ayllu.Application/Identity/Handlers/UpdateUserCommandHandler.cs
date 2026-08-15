using Ayllu.Application.Common.Abstractions.Identity;
using Ayllu.Application.Identity.Commands;
using Ayllu.Application.Identity.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Identity.Handlers;

public sealed class UpdateUserCommandHandler(IIdentityService @is) : IRequestHandler<UpdateUserCommand, ApplicationUserResponse>
{
    public async Task<ApplicationUserResponse> Handle(IReceiveContext<UpdateUserCommand> context, CancellationToken cancellationToken)
        => await @is.UpdateUserAsync(context.Message.Request, context.Message.UserId, cancellationToken);
}
