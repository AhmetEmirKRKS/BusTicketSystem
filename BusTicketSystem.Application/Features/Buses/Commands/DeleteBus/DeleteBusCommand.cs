using MediatR;
using System;

namespace BusTicketSystem.Application.Features.Buses.Commands.DeleteBus
{
    public class DeleteBusCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
