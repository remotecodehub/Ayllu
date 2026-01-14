using AutoMapper;
using Ayllu.Web.Application.Common.Abstractions.Identity;
using Ayllu.Web.Application.Identity.Queries;
using Ayllu.Web.Application.Identity.Responses;
using MediatR;

namespace Ayllu.Web.Application.Identity.Handlers;

public sealed class GetCurrentUserQueryHandler(IIdentityService @is) : IRequestHandler<GetCurrentUserQuery, ApplicationUserResponse>
{
    public async Task<ApplicationUserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken) 
        => await @is.GetCurrentUserAsync(request.UserId, cancellationToken);
}
