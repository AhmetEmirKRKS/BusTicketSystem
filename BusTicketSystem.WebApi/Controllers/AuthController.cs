using Microsoft.AspNetCore.Mvc;
using BusTicketSystem.Application.Features.Auth.Commands.Login;
using BusTicketSystem.Application.Features.Auth.Commands.Register;
using MediatR;
using System.Threading.Tasks;
using BusTicketSystem.Application.Features.Auth.Commands.VerifyEmail;

namespace BusTicketSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
