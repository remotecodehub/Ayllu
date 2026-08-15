using Ayllu.Application.Dialectics.Requests;
using Ayllu.Application.Dialectics.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Dialectics.Commands;

public sealed record CreateDialecticCommand(
    CreateDialecticRequest Request,
    string UserId
) : IRequest<CreateDialecticResponse>;
