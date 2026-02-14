using System;

namespace BusTicketSystem.Application.DTOs
{
    public class BusDto
    {
        public Guid Id { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int TotalSeats { get; set; }

        public string SeatLayout { get; set; } = string.Empty;
    }
}
