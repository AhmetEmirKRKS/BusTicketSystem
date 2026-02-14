using BusTicketSystem.Application.Features.Trips.Commands.CreateTrip;
using BusTicketSystem.Application.Features.Trips.Queries.GetAllTrips;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BusTicketSystem.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TripsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTripCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTripsQuery());
            return Ok(result);
        }
    }
}
