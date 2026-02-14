using System;
using MediatR;

namespace BusTicketSystem.Application.Features.Routes.Commands.UpdateRoute
{
    public class UpdateRouteCommand : IRequest
    {
        public Guid Id { get; set; }
        public string DepartureCity { get; set; } = string.Empty;
        public string ArrivalCity { get; set; } = string.Empty;
        public int EstimatedDurationMinutes { get; set; } 
    }
}
