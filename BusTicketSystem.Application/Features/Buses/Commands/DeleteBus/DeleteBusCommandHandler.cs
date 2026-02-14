using BusTicketSystem.Application.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Features.Buses.Commands.DeleteBus
{
    public class DeleteBusCommandHandler : IRequestHandler<DeleteBusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteBusCommand request, CancellationToken cancellationToken)
        {
            var bus = await _unitOfWork.BusRepository.GetByIdAsync(request.Id);

            if (bus == null)
                throw new Exception("Silinecek otobüs bulunamadı");

            _unitOfWork.BusRepository.Remove(bus);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
