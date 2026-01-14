using Ayllu.Web.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Web.Application.Dialectics.Queries;

public sealed record GetDialecticByIdQuery(
    string DialecticId,
    string UserId
) : IRequest<DialecticResponse>;
