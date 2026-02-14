using BusTicketSystem.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace BusTicketSystem.Application.Features.Buses.Queries.GetAllBuses
{
    public class GetAllBusesQuery : IRequest<List<BusDto>>
    {

    }
}
