using BusTicketSystem.Application.Interfaces.UnitOfWork;
using BusTicketSystem.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Features.Trips.Commands.CreateTrip
{
    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTripCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            var bus = await _unitOfWork.BusRepository.GetByIdAsync(request.BusId);
            if (bus == null)
                throw new Exception("Seçilen otobüs sistemde bulunamadı");

            var route = await _unitOfWork.RouteRepository.GetByIdAsync(request.RouteId);
            if (route == null)
                throw new Exception("Seçilen güzergah sistemde bulunamadı");

            var trip = new Trip(
                request.BusId,
                request.RouteId,
                request.DepartureDate,
                request.Price
                );

            await _unitOfWork.TripRepository.AddAsync(trip);
            await _unitOfWork.SaveChangesAsync();

            return trip.Id;
        }
    }
}
