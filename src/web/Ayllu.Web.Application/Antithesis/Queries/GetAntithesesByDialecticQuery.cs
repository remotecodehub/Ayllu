using Ayllu.Web.Application.Antithesis.Responses;
using MediatR;

namespace Ayllu.Web.Application.Antithesis.Queries;

public sealed record GetAntithesesByDialecticQuery(
    string DialecticId,
    string UserId,
    bool PublicAntithesisOnly
) : IRequest<AntithesisListResponse>;
