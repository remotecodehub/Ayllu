using Ayllu.Web.Application.Common.Abstractions.Identity;
using Ayllu.Web.Application.Identity.Commands;
using Ayllu.Web.Application.Identity.Responses;
using MediatR;

namespace Ayllu.Web.Application.Identity.Handlers;

public sealed class UpdateUserCommandHandler(IIdentityService @is) : IRequestHandler<UpdateUserCommand, ApplicationUserResponse>
{
    public async Task<ApplicationUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken) => await @is.UpdateUserAsync(request.Request, request.UserId, cancellationToken);
}
