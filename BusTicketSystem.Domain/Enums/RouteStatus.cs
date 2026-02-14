namespace BusTicketSystem.Domain.Enums
{
    public enum RouteStatus
    {
        Active = 1, //sefer düzenlenebilir
        Passive = 0, //şu an bu hat çalışmıyor
        UnderMaintenance = 2 //yol çalışması vb. var 
    }
}
