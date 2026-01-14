using Ayllu.Web.Application.Thesis.Requests;
using Ayllu.Web.Application.Thesis.Responses;
using MediatR;

namespace Ayllu.Web.Application.Thesis.Commands;

public sealed record UpdateThesisCommand(
    string DialecticId,
    UpdateThesisRequest Request,
    string UserId,
    string ThesisId
) : IRequest<ThesisResponse>;
