using Ayllu.Web.Application.Common.Abstractions.Thesis;
using Ayllu.Web.Application.Thesis.Commands;
using Ayllu.Web.Application.Thesis.Responses;
using MediatR;

namespace Ayllu.Web.Application.Thesis.Handlers;

public sealed class UpdateThesisCommandHandler(IThesisService ts)
    : IRequestHandler<UpdateThesisCommand, ThesisResponse>
{
    public async Task<ThesisResponse> Handle(UpdateThesisCommand request, CancellationToken cancellationToken) 
        => await ts.UpdateThesisAsync(request.Request.Content, request.UserId, request.DialecticId, request.ThesisId, cancellationToken);
}
