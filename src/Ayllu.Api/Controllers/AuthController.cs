using Ayllu.Application.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ayllu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registra um novo usuário e realiza login automático
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand request)
        {
            var response = await _mediator.Send(request);
            return response.StatusCode switch
            {
                201 => CreatedAtAction(nameof(Login), new { email = request.Email }, response),
                400 => BadRequest(response),
                409 => Conflict(response),
                422 => UnprocessableEntity(response),
                _ => StatusCode(response.StatusCode, response)
            };
        }

        /// <summary>
        /// Login do usuário
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromHeader] string token)
        {
            var request = new LoginCommand(token);
            var response = await _mediator.Send(request);
            return response.StatusCode switch
            {
                200 => Ok(response),
                400 => BadRequest(response),
                401 => Unauthorized(response),
                404 => NotFound(response),
                _ => StatusCode(response.StatusCode, response)
            };
        }

        ///// <summary>
        ///// Logout do usuário (invalida JWT)
        ///// </summary>
        //[Authorize]
        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    // Obtém o JWT do header
        //    var jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        //    var command = new LogoutCommand(jwt);

        //    var response = await _mediator.Send(command);

        //    if (!response.Success)
        //        return BadRequest(response);

        //    return Ok(response);
        //}
    }
}
