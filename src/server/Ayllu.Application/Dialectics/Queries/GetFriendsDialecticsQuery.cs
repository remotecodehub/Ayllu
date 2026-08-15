using Ayllu.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Application.Dialectics.Queries;

public sealed record GetFriendsDialecticsQuery(
    string UserId
) : IRequest<DialecticListResponse>;