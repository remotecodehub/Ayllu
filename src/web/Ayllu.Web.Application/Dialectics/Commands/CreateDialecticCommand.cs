using Ayllu.Web.Application.Dialectics.Requests;
using Ayllu.Web.Application.Dialectics.Responses;
using MediatR;

namespace Ayllu.Web.Application.Dialectics.Commands;

public sealed record CreateDialecticCommand(
    CreateDialecticRequest Request,
    string UserId
) : IRequest<CreateDialecticResponse>;