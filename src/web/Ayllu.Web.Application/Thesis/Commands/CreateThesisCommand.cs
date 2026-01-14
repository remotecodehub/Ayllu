using Ayllu.Web.Application.Thesis.Requests;
using Ayllu.Web.Application.Thesis.Responses;
using MediatR;

namespace Ayllu.Web.Application.Thesis.Commands;

public sealed record CreateThesisCommand(
    string DialecticId,
    CreateThesisRequest Request,
    string UserId
) : IRequest<ThesisResponse>;