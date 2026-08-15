using Ayllu.Application.Common.Abstractions.Synthesis;
using Ayllu.Application.Synthesis.Queries;
using Ayllu.Application.Synthesis.Responses;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Synthesis.Handlers;

public sealed class GetSynthesesByDialecticQueryHandler(ISynthesisService ss) : IRequestHandler<GetSynthesesByDialecticQuery, SynthesisListResponse>
{
    public async Task<SynthesisListResponse> Handle(IReceiveContext<GetSynthesesByDialecticQuery> context, CancellationToken cancellationToken)
        => await ss.GetSynthesesByDialecticAsync(context.Message.UserId, context.Message.DialecticId, cancellationToken);
}
