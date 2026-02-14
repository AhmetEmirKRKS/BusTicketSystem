using System;
using BusTicketSystem.Domain.Common;
using BusTicketSystem.Domain.Enums;

namespace BusTicketSystem.Domain.Entities
{
    public class Route : BaseEntity
    {
        public string DepartureCity { get; private set; } = string.Empty; //kalkış şehri
        public string ArrivalCity { get; private set; } = string.Empty; //varış şehri
        public int EstimatedDurationMinutes { get; private set; } //tahmini süre(dk)

        public RouteStatus Status { get; private set; }
        public Route() { }
        public Route(string departure, string arrival, int duration)
        {
            if (string.IsNullOrEmpty(departure) || string.IsNullOrEmpty(arrival))
                throw new Exception("Kalkış ve varış yerleri boş olamaz");

            DepartureCity = departure;
            ArrivalCity = arrival;
            EstimatedDurationMinutes = duration;
            Status = RouteStatus.Active;
        }

        public void DeactiveRoute()
        {
            Status = RouteStatus.Passive;
            UpdatedDate = DateTime.UtcNow;
        }

        public void ActivateRoute()
        {
            Status = RouteStatus.Active;
            UpdatedDate = DateTime.UtcNow;
        }

        public void UpdateDetails(string departure, string arrival, int duration)
        {
            if (string.IsNullOrEmpty(departure) || string.IsNullOrEmpty(arrival))
                throw new Exception("Kalkış ve varış yerleri boş olamaz");

            DepartureCity = departure;
            ArrivalCity = arrival;
            EstimatedDurationMinutes = duration;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
