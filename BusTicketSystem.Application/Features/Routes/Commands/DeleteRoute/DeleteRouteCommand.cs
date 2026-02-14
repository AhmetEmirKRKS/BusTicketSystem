using System;
using MediatR;

namespace BusTicketSystem.Application.Features.Routes.Commands.DeleteRoute
{
    public class DeleteRouteCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
