using BusTicketSystem.Application.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Features.Routes.Commands.DeleteRoute
{
    public class DeleteRouteCommandHandler : IRequestHandler<DeleteRouteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRouteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteRouteCommand request, CancellationToken cancellationToken)
        {
            var route = await _unitOfWork.RouteRepository.GetByIdAsync(request.Id);
            if (route == null) throw new Exception("Rota bulunamadı");

            _unitOfWork.RouteRepository.Remove(route);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
