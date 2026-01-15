using Ayllu.Web.Application.Antithesis.Requests;
using Ayllu.Web.Application.Antithesis.Responses;
using MediatR;

namespace Ayllu.Web.Application.Antithesis.Commands;

public sealed record CreateAntithesisCommand(
    string DialecticId,
    CreateAntithesisRequest Request,
    string UserId
) : IRequest<AntithesisResponse>;