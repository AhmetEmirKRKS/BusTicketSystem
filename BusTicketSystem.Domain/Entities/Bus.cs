using System;
using BusTicketSystem.Domain.Common;
using BusTicketSystem.Domain.Enums;

namespace BusTicketSystem.Domain.Entities
{
    public class Bus : BaseEntity
    {
        //property'lerin "set" kısımlarını private yapıyoruz(encapsulation)
        public string PlateNumber { get; private set; } = string.Empty; //plaka
        public string Model { get; private set; } = string.Empty; //otobüs modeli
        public int TotalSeats { get; private set; }  //koltuk sayısı

        public SeatLayout SeatLayout { get; private set; } //Enum
        public Bus() { } //entity framework için boş constructor gereklidir

        public Bus(string plateNumber, string model, int totalSeats, SeatLayout seatLayout)
        {
            if (totalSeats < 10)
                throw new Exception("Bir otobüsün koltuk sayısı 10'dan az olamaz!");

            if (seatLayout == SeatLayout.Suite_2_1 && totalSeats > 50)
                throw new Exception("2+1 otobüslerde koltuk sayısı 50'yi geçemez");

            PlateNumber = plateNumber;
            Model = model;
            TotalSeats = totalSeats;
            SeatLayout = seatLayout;
        }

        public void UpdateDetails(string plateNumber, string model, int totalSeats, SeatLayout seatLayout)
        {
            if (string.IsNullOrEmpty(plateNumber)) throw new Exception("Plaka boş olamaz");
            if (totalSeats < 10) throw new Exception("Koltık sayısı 10'dan az olamaz");

            PlateNumber = plateNumber;
            Model = model;
            TotalSeats = totalSeats;
            SeatLayout = seatLayout;

            UpdatedDate = DateTime.UtcNow;
        }

    }
}
