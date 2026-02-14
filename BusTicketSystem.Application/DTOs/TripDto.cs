using System;

namespace BusTicketSystem.Application.DTOs
{
    public class TripDto
    {
        public Guid Id { get; set; }
        public DateTime DepartureDate { get; set; }
        public decimal Price { get; set; }

        //İlişkili tablolar
        public string BusPlateNumber { get; set; } = string.Empty;
        public string BusModel { get; set; } = string.Empty;

        public string RouteName { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
    }
}
