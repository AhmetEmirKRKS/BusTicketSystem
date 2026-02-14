using MediatR;
using System;
using BusTicketSystem.Domain.Enums;

namespace BusTicketSystem.Application.Features.Buses.Commands.UpdateBus
{
    public class UpdateBusCommand : IRequest
    {
        public Guid Id { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public SeatLayout SeatLayout { get; set; }
    }
}
