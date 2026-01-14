using Ayllu.Web.Application.Antithesis.Queries;
using Ayllu.Web.Application.Antithesis.Responses;
using Ayllu.Web.Application.Common.Abstractions.Antithesis;
using MediatR;

namespace Ayllu.Web.Application.Antithesis.Handlers;

public sealed class GetAntithesesByDialecticQueryHandler(IAntithesisService ats) : IRequestHandler<GetAntithesesByDialecticQuery, AntithesisListResponse>
{
    public async Task<AntithesisListResponse> Handle(GetAntithesesByDialecticQuery request, CancellationToken cancellationToken)
        => await ats.GetAntithesesByDialectAsync(request.UserId, request.DialecticId, request.PublicAntithesisOnly, cancellationToken);
}
