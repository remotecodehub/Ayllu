using Ayllu.Application.Antithesis.Queries;
using Ayllu.Application.Antithesis.Responses;
using Ayllu.Application.Common.Abstractions.Antithesis;
using MediatR;

namespace Ayllu.Application.Antithesis.Handlers;

public sealed class GetAntithesesByDialecticQueryHandler(IAntithesisService ats) : IRequestHandler<GetAntithesesByDialecticQuery, AntithesisListResponse>
{
    public async Task<AntithesisListResponse> Handle(GetAntithesesByDialecticQuery request, CancellationToken cancellationToken)
        => await ats.GetAntithesesByDialectAsync(request.UserId, request.DialecticId, request.PublicAntithesisOnly, cancellationToken);
}
