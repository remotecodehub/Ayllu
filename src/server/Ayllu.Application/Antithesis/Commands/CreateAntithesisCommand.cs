using Ayllu.Application.Antithesis.Requests;
using Ayllu.Application.Antithesis.Responses;
using MediatR;

namespace Ayllu.Application.Antithesis.Commands;

public sealed record CreateAntithesisCommand(
    string DialecticId,
    CreateAntithesisRequest Request,
    string UserId
) : IRequest<AntithesisResponse>;