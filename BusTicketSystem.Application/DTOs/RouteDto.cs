using System;

namespace BusTicketSystem.Application.DTOs
{
    public class RouteDto
    {
        public Guid Id { get; set; }
        public string DepartureCity { get; set; } = string.Empty;
        public string ArrivalCity { get; set; } = string.Empty;
        public int EstimatedDurationMinutes { get; set; }
    }
}
