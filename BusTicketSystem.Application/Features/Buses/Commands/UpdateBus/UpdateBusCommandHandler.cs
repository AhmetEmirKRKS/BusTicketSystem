using BusTicketSystem.Application.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Features.Buses.Commands.UpdateBus
{
    public class UpdateBusCommandHandler : IRequestHandler<UpdateBusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateBusCommand request, CancellationToken cancelletionToken)
        {
            var bus = await _unitOfWork.BusRepository.GetByIdAsync(request.Id);

            if (bus == null)
                throw new Exception("Güncellenecek otobüs bulunamadı");

            bus.UpdateDetails(  //Entity içindeki metodu kullanarak güncelle
                request.PlateNumber,
                request.Model,
                request.TotalSeats,
                request.SeatLayout
                );

            _unitOfWork.BusRepository.Update(bus);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
