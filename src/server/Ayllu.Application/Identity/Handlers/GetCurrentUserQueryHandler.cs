using Ayllu.Application.Common.Abstractions.Identity;
using Ayllu.Application.Identity.Queries;
using Ayllu.Application.Identity.Responses;
using MediatR;

namespace Ayllu.Application.Identity.Handlers;

public sealed class GetCurrentUserQueryHandler(IIdentityService @is) : IRequestHandler<GetCurrentUserQuery, ApplicationUserResponse>
{
    public async Task<ApplicationUserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken) 
        => await @is.GetCurrentUserAsync(request.UserId, cancellationToken);
}
