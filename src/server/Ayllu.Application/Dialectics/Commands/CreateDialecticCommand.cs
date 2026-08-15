using Ayllu.Application.Dialectics.Requests;
using Ayllu.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Application.Dialectics.Commands;

public sealed record CreateDialecticCommand(
    CreateDialecticRequest Request,
    string UserId
) : IRequest<CreateDialecticResponse>;