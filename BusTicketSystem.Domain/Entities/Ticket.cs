using System;
using BusTicketSystem.Domain.Common;
using BusTicketSystem.Domain.Enums;

namespace BusTicketSystem.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public Guid TripId { get; private set; }
        public Guid PassengerId { get; private set; }
        public int SeatNumber { get; private set; }
        public decimal PricePaid { get; private set; }
        public TicketStatus Status { get; private set; }

        //Navigation Properties
        public virtual Trip Trip { get; private set; } = null!;
        public virtual Passenger Passenger { get; private set; } = null!;

        public Ticket () { }

        public Ticket(Guid tripId, Guid passengerId, int seatNumber, decimal pricePaid)
        {
            if (seatNumber <= 0)
                throw new Exception("Geçersiz koltuk numarası");
            if (pricePaid < 0)
                throw new Exception("Bilet fiyatı nergatif olamaz");

            TripId = tripId;
            PassengerId = passengerId;
            SeatNumber = seatNumber;
            PricePaid = pricePaid;
            Status = TicketStatus.Sold; //ilk oluşturulduğunda satıldı kabul ediyoruz
        }

        public void CancelTicket()
        {
            if (Status == TicketStatus.Cancelled)
                throw new Exception("Zaten iptal edilmiş bir bilet tekrar iptal edilemez");

            Status = TicketStatus.Cancelled;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
