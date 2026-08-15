using Ayllu.Application.Dialectics.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Dialectics.Commands;

public sealed record CloseDialecticCommand(
    string DialecticId,
    string UserId
) : IRequest<DialecticResponse?>;
