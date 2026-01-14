using Ayllu.Web.Application.Common.Abstractions.Synthesis;
using Ayllu.Web.Application.Synthesis.Queries;
using Ayllu.Web.Application.Synthesis.Responses;
using MediatR;

namespace Ayllu.Web.Application.Synthesis.Handlers;

public sealed class GetSynthesesByDialecticQueryHandler(ISynthesisService ss) : IRequestHandler<GetSynthesesByDialecticQuery, SynthesisListResponse>
{
    public async Task<SynthesisListResponse> Handle(GetSynthesesByDialecticQuery request, CancellationToken cancellationToken) 
        => await ss.GetSynthesesByDialecticAsync(request.UserId, request.DialecticId, cancellationToken);
}
