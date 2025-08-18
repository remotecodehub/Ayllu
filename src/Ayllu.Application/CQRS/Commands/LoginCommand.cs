using Ayllu.Application.DTO.Responses;
using MediatR;

namespace Ayllu.Application.CQRS.Commands;

public record LoginCommand (string Token) : IRequest<Response<LoginResponse>>;
