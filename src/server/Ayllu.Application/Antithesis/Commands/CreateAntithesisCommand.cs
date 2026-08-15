using Ayllu.Application.Antithesis.Requests;
using Ayllu.Application.Antithesis.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Antithesis.Commands;

public sealed record CreateAntithesisCommand(
    string DialecticId,
    CreateAntithesisRequest Request,
    string UserId
) : IRequest<AntithesisResponse>;
