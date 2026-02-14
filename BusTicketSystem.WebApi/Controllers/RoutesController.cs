using BusTicketSystem.Application.Features.Buses.Queries.GetAllBuses;
using BusTicketSystem.Application.Features.Routes.Commands.CreateRoute;
using BusTicketSystem.Application.Features.Routes.Commands.DeleteRoute;
using BusTicketSystem.Application.Features.Routes.Commands.UpdateRoute;
using BusTicketSystem.Application.Features.Routes.Queries.GetAllRoutes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BusTicketSystem.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoutesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateRouteCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllRoutesQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateRouteCommand command)
        {
            if (id != command.Id) return BadRequest("ID uyuşmazlığı");
            await _mediator.Send(command);
            return Ok(new { Message = "Rota güncellendi" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteRouteCommand { Id = id });
            return Ok(new { Message = "Rota silinid" });
        }
    }
}
