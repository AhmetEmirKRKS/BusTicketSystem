using BusTicketSystem.Application.Features.Buses.Commands.CreateBus;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BusTicketSystem.Application.Features.Buses.Queries.GetAllBuses;
using BusTicketSystem.Application.Features.Buses.Commands.UpdateBus;
using BusTicketSystem.Application.Features.Buses.Commands.DeleteBus;
using Microsoft.AspNetCore.Authorization;

namespace BusTicketSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBusCommand command)
        {
            var createBusId = await _mediator.Send(command);

            return Ok(createBusId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllBusesQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateBusCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID uyuşmazlığı");

            await _mediator.Send(command);
            return Ok(new { Message = "Otobüs başarıyla güncellendi" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteBusCommand { Id = id });
            return Ok(new { Message = "Otobüs başarıyla silindi" });
        }
    }
}
