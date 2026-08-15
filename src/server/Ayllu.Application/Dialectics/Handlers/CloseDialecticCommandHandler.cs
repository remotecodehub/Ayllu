using Ayllu.Application.Common.Abstractions.Dialectic;
using Ayllu.Application.Dialectics.Commands;
using Ayllu.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Application.Dialectics.Handlers;

public sealed class CloseDialecticCommandHandler(IDialecticService ds) : IRequestHandler<CloseDialecticCommand, DialecticResponse?>
{
    public async Task<DialecticResponse?> Handle(CloseDialecticCommand request, CancellationToken cancellationToken)
    {
        return await ds.CloseDialecticAsync(request.DialecticId, request.UserId, cancellationToken);
    }
}
