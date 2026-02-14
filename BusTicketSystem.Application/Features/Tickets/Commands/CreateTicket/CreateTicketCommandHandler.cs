using BusTicketSystem.Application.Interfaces.UnitOfWork;
using BusTicketSystem.Domain.Entities;
using BusTicketSystem.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusTicketSystem.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTicketCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(request.TripId);
            if (trip == null)
                throw new Exception("Sefer bulunamadı");

            var bus = await _unitOfWork.BusRepository.GetByIdAsync(trip.BusId);
            if (bus == null) throw new Exception("Seferin tanımlı olduğu otobüs bulunamadı");

            var soldTickets = await _unitOfWork.TicketRepository.GetWhereAsync(x =>
                x.TripId == request.TripId &&
                x.SeatNumber == request.SeatNumber &&
                x.Status != TicketStatus.Cancelled
            );

            if (soldTickets.Any())
                throw new Exception($"Seçilen {request.SeatNumber} numaralı koltuk maalesef dolu");

            //Dynamic Pricing
            int soldCount = soldTickets.Count; //satılan bilet sayısı
            double occupancyRate = (double)soldCount / bus.TotalSeats; //doluluk oranı
            decimal finalPrice = trip.Price; //başlangıç fiyatı

            if(occupancyRate > 0.80)
            {
                finalPrice = trip.Price * 1.20m;
            }
            else if(occupancyRate > 0.50)
            {
                finalPrice = trip.Price * 1.10m;
            }

                var passenger = new Passenger(
                    request.PassengerFirstName,
                    request.PassengerLastName,
                    request.PassengerNationalId,
                    request.PassengerGender,
                    request.PassengerPhone,
                    request.PassengerEmail
                 );

            await _unitOfWork.PassengerRepository.AddAsync(passenger);

            var ticket = new Ticket(
                request.TripId,
                passenger.Id,
                request.SeatNumber,
                trip.Price
            );

            await _unitOfWork.TicketRepository.AddAsync(ticket);

            trip.UpdatedDate = DateTime.UtcNow; //OCC için (updateDate baseEnitity'den geliyor)
            _unitOfWork.TripRepository.Update(trip);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) //EF core ' un bize verdiği bir özellik
            {
                throw new Exception("Şu anda koltuk seçiminde bir yoğunluk var, lütfen tekrar deneyiniz");
            }

            return ticket.Id;
        }
    }
}
