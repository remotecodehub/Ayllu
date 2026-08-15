using Ayllu.Application.Thesis.Requests;
using Ayllu.Application.Thesis.Responses;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Thesis.Commands;

public sealed record CreateThesisCommand(
    string DialecticId,
    CreateThesisRequest Request,
    string UserId
) : IRequest;
