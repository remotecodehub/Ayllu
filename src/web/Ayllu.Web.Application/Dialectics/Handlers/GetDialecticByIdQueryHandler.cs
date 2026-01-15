using Ayllu.Web.Application.Common.Abstractions.Dialectic;
using Ayllu.Web.Application.Dialectics.Queries;
using Ayllu.Web.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Web.Application.Dialectics.Handlers;

public sealed class GetDialecticByIdQueryHandler(IDialecticService ds)
    : IRequestHandler<GetDialecticByIdQuery, DialecticResponse?>
{
    public async Task<DialecticResponse?> Handle(GetDialecticByIdQuery request, CancellationToken cancellationToken) 
        => await ds.GetDialecticByIdAsync(request.DialecticId, cancellationToken);
    
}