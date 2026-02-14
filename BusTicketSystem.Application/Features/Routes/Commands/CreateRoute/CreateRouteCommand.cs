using System;
using MediatR;

namespace BusTicketSystem.Application.Features.Routes.Commands.CreateRoute
{
    public class CreateRouteCommand : IRequest<Guid>
    {
        public string DepartureCity { get; set; } = string.Empty; //kalkış
        public string ArrivalCity { get; set; } = string.Empty; //varış
        public int EstimatedDurationMinutes { get; set; } //süre
    }
}
