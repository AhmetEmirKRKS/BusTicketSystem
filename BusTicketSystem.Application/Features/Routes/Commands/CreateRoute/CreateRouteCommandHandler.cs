using MediatR;
using BusTicketSystem.Application.Interfaces.UnitOfWork;
using BusTicketSystem.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Features.Routes.Commands.CreateRoute
{
    public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRouteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {
            var route = new Route(
                request.DepartureCity,
                request.ArrivalCity,
                request.EstimatedDurationMinutes
                );

            await _unitOfWork.RouteRepository.AddAsync(route);
            await _unitOfWork.SaveChangesAsync();

            return route.Id;

        }
    }
}
