using Ayllu.Application.Antithesis.Commands;
using Ayllu.Application.Antithesis.Responses;
using Ayllu.Application.Common.Abstractions.Antithesis;
using MediatR;

namespace Ayllu.Application.Antithesis.Handlers;

public sealed class CreateAntithesisCommandHandler(IAntithesisService ats)
 : IRequestHandler<CreateAntithesisCommand, AntithesisResponse>
{
    public async Task<AntithesisResponse> Handle(CreateAntithesisCommand request, CancellationToken cancellationToken) 
        => await ats.CreateAntithesisAsync(request.UserId, request.DialecticId, request.Request.Content, cancellationToken);
}
