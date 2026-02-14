namespace BusTicketSystem.Domain.Enums
{
    public enum TicketStatus
    {
        Sold = 1, //satıldı
        Reserved = 2, //rezerve edildi(ödeme bekleniyor)
        Cancelled = 0 //iptal edildi
    }
}
