using Ayllu.Application.Common.Abstractions.Synthesis;
using Ayllu.Application.Synthesis.Queries;
using Ayllu.Application.Synthesis.Responses;
using MediatR;

namespace Ayllu.Application.Synthesis.Handlers;

public sealed class GetSynthesesByDialecticQueryHandler(ISynthesisService ss) : IRequestHandler<GetSynthesesByDialecticQuery, SynthesisListResponse>
{
    public async Task<SynthesisListResponse> Handle(GetSynthesesByDialecticQuery request, CancellationToken cancellationToken) 
        => await ss.GetSynthesesByDialecticAsync(request.UserId, request.DialecticId, cancellationToken);
}
