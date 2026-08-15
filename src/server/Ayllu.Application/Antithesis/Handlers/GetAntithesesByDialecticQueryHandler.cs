using Ayllu.Application.Antithesis.Queries;
using Ayllu.Application.Antithesis.Responses;
using Ayllu.Application.Common.Abstractions.Antithesis;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Antithesis.Handlers;

public sealed class GetAntithesesByDialecticQueryHandler(IAntithesisService ats) : IRequestHandler<GetAntithesesByDialecticQuery, AntithesisListResponse>
{
    public async Task<AntithesisListResponse> Handle(IReceiveContext<GetAntithesesByDialecticQuery> context, CancellationToken cancellationToken)
        => await ats.GetAntithesesByDialectAsync(context.Message.UserId, context.Message.DialecticId, context.Message.PublicAntithesisOnly, cancellationToken);
}
