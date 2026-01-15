using Ayllu.Web.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Web.Application.Dialectics.Commands;

public sealed record CloseDialecticCommand(
    string DialecticId,
    string UserId
) : IRequest<DialecticResponse?>;