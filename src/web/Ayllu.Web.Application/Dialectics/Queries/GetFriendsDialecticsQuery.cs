using Ayllu.Web.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Web.Application.Dialectics.Queries;

public sealed record GetFriendsDialecticsQuery(
    string UserId
) : IRequest<DialecticListResponse>;