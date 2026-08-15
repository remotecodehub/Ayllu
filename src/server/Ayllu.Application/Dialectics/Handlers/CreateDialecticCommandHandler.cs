using Ayllu.Application.Common.Abstractions.Dialectic;
using Ayllu.Application.Dialectics.Commands;
using Ayllu.Application.Dialectics.Responses;
using MediatR;
namespace Ayllu.Application.Dialectics.Handlers;

public sealed class CreateDialecticCommandHandler(IDialecticService ds)
    : IRequestHandler<CreateDialecticCommand, CreateDialecticResponse>
{
    public async Task<CreateDialecticResponse> Handle(
        CreateDialecticCommand request,
        CancellationToken cancellationToken)
    {
        var dialectic = await ds.CreateDialecticAsync(request.Request.Title, request.Request.Description, request.Request.IsPublic, request.UserId, cancellationToken);

        return new CreateDialecticResponse(dialectic.DialecticId);
    }
}