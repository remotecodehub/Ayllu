using Ayllu.Application.Thesis.Requests;
using Ayllu.Application.Thesis.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Thesis.Commands;

public sealed record UpdateThesisCommand(
    string DialecticId,
    UpdateThesisRequest Request,
    string UserId,
    string ThesisId
) : IRequest<ThesisResponse>;
