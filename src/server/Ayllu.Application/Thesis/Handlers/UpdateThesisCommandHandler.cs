using Ayllu.Application.Common.Abstractions.Thesis;
using Ayllu.Application.Thesis.Commands;
using Ayllu.Application.Thesis.Responses;
using MediatR;

namespace Ayllu.Application.Thesis.Handlers;

public sealed class UpdateThesisCommandHandler(IThesisService ts)
    : IRequestHandler<UpdateThesisCommand, ThesisResponse>
{
    public async Task<ThesisResponse> Handle(UpdateThesisCommand request, CancellationToken cancellationToken) 
        => await ts.UpdateThesisAsync(request.Request.Content, request.UserId, request.DialecticId, request.ThesisId, cancellationToken);
}
