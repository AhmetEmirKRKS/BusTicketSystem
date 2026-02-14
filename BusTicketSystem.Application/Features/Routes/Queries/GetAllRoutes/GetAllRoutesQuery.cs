using BusTicketSystem.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace BusTicketSystem.Application.Features.Routes.Queries.GetAllRoutes
{
    public class GetAllRoutesQuery : IRequest<List<RouteDto>>
    {
    }
}
