using Ayllu.Application.Common.Abstractions.Thesis;
using Ayllu.Application.Thesis.Commands;
using Ayllu.Application.Thesis.Responses;
using MediatR;

namespace Ayllu.Application.Thesis.Handlers;

public sealed class CreateThesisCommandHandler(IThesisService ts)
    : IRequestHandler<CreateThesisCommand, ThesisResponse>
{
    public async Task<ThesisResponse> Handle(CreateThesisCommand request, CancellationToken cancellationToken) => await ts.CreateThesisAsync(request.Request.Content, request.UserId, request.DialecticId, cancellationToken);
}
