using Ayllu.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Application.Dialectics.Queries;

public sealed record GetDialecticByIdQuery(
    string DialecticId,
    string UserId
) : IRequest<DialecticResponse>;
