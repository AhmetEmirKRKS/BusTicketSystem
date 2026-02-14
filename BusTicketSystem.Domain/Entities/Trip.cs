using System;
using BusTicketSystem.Domain.Common;
using BusTicketSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations; //timestamp için gerekli

namespace BusTicketSystem.Domain.Entities
{
    public class Trip : BaseEntity
    {
        public Guid BusId { get; private set; }
        public Guid RouteId { get; private set; }
        public DateTime DepartureDate { get; private set; } //Kalkış zamanı
        public decimal Price { get; private set; }

        public TripStatus Status { get; private set; } //Enum kullanımı

        //DDD'de bu nesneleri "virtual" işaretleriz ki EF Core Lazy Loading yapabilsin (gerekirse)
        public virtual Bus Bus { get; private set; } = null!; //Navigation Properties'ler
        public virtual Route Route { get; private set; } = null!;

        [Timestamp]  //çakışma kontrolü için damga
        public byte[] RowVersion { get; set; } = null!;
        public Trip() { }

        public Trip(Guid busId, Guid routeId, DateTime departureDate, decimal price)
        {
            if (departureDate < DateTime.UtcNow)
                throw new Exception("Geçmiş bir tarihe sefer düzenlenemez");

            if (price <= 0)
                throw new Exception("Bilet fiyatı 0 veya daha düşük olamaz");

            BusId = busId;
            RouteId = routeId;
            DepartureDate = departureDate;
            Price = price;

            Status = TripStatus.Scheduled; //varsayılan olarak planladı statüsünde başlancak
        }

        //İş kuralları
        public void CancelTrip() //sefer iptal etme metodu
        {
            if (Status == TripStatus.Completed)
                throw new Exception("Tamamlanmış bir sefer iptal edilemez");

            Status = TripStatus.Cancelled;
            UpdatedDate = DateTime.UtcNow;
        }

        public void CompleteTrip() //sefer tamamlandı
        {
            if (Status == TripStatus.Cancelled)
                throw new Exception("İptal edilmiş bir sefer tamamlanamaz");

            Status = TripStatus.Completed;
            UpdatedDate = DateTime.UtcNow;
        }

        public void UpdatePrice(decimal newPrice) //fiyat güncelleme
        {
            if (newPrice <= 0) throw new Exception("Geçersiz fiyat");
            if (Status == TripStatus.Completed || Status == TripStatus.Cancelled)
                throw new Exception("Bitmiş veya iptal olmuş bir seferin fiyatı değişemez");

            Price = newPrice;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
