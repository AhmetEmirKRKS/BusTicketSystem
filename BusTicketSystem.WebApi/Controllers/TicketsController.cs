using BusTicketSystem.Application.Features.Tickets.Commands.CreateTicket;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BusTicketSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> BuyTicket(CreateTicketCommand command)
        {
            var ticketId = await _mediator.Send(command);
            return Ok(new { Message = "Bilet başarıyla oluşturuldu", TicketId = ticketId });
        }
    }
}
