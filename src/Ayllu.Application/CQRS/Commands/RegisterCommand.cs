using Ayllu.Application.DTO.Responses;
using MediatR;

namespace Ayllu.Application.CQRS.Commands;

public record RegisterCommand(string UserName,
    string Email,
    string Password,
    string Name,
    string LastName,
    string PhoneNumber) : IRequest<Response<LoginResponse>> ;
