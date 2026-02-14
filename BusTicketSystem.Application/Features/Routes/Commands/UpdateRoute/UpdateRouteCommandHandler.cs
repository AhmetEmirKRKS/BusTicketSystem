using MediatR;
using BusTicketSystem.Application.Interfaces.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Features.Routes.Commands.UpdateRoute
{
    public class UpdateRouteCommandHandler : IRequestHandler<UpdateRouteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateRouteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateRouteCommand request, CancellationToken cancellationToken)
        {
            var route = await _unitOfWork.RouteRepository.GetByIdAsync(request.Id);
            if (route == null) throw new Exception("Rota bulunamadı");

            route.UpdateDetails(request.DepartureCity, request.ArrivalCity, request.EstimatedDurationMinutes);

            _unitOfWork.RouteRepository.Update(route);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
