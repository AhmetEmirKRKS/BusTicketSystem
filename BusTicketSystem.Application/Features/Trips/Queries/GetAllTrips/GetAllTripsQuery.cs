using BusTicketSystem.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace BusTicketSystem.Application.Features.Trips.Queries.GetAllTrips
{
    public class GetAllTripsQuery : IRequest<List<TripDto>>
    {
    }
}
