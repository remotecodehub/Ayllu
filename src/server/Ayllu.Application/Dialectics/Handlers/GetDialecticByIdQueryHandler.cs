using Ayllu.Application.Common.Abstractions.Dialectic;
using Ayllu.Application.Dialectics.Queries;
using Ayllu.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Application.Dialectics.Handlers;

public sealed class GetDialecticByIdQueryHandler(IDialecticService ds)
    : IRequestHandler<GetDialecticByIdQuery, DialecticResponse?>
{
    public async Task<DialecticResponse?> Handle(GetDialecticByIdQuery request, CancellationToken cancellationToken) 
        => await ds.GetDialecticByIdAsync(request.DialecticId, cancellationToken);
    
}