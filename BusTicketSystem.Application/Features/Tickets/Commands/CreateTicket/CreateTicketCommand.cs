using System;
using MediatR;
using BusTicketSystem.Domain.Enums;

namespace BusTicketSystem.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommand : IRequest<Guid>
    {
        public Guid TripId { get; set; }
        public int SeatNumber { get; set; }

        public string PassengerFirstName { get; set; } = string.Empty;
        public string PassengerLastName { get; set; } = string.Empty;
        public string PassengerNationalId { get; set; } = string.Empty;
        public Gender PassengerGender { get; set; }
        public string PassengerPhone { get; set; } = string.Empty;
        public string? PassengerEmail { get; set; }
    }
}
