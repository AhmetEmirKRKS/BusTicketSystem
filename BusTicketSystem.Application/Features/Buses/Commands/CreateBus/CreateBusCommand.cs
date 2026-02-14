using System;
using MediatR;
using BusTicketSystem.Domain.Enums;

namespace BusTicketSystem.Application.Features.Buses.Commands.CreateBus
{
    public class CreateBusCommand : IRequest<Guid>
    {
        public string PlateNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public SeatLayout SeatLayout { get; set; } //Enum'ı direk alabiliriz 
    }
}
