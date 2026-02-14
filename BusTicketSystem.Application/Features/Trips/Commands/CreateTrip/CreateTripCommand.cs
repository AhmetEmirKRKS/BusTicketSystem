using System;
using MediatR;

namespace BusTicketSystem.Application.Features.Trips.Commands.CreateTrip
{
    public class CreateTripCommand : IRequest<Guid>
    {
        public Guid BusId { get; set; }
        public Guid RouteId { get; set; }
        public DateTime DepartureDate { get; set; }
        public decimal Price { get; set; }
    }
}
