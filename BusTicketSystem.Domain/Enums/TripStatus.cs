namespace BusTicketSystem.Domain.Enums
{
    public enum TripStatus
    {
        Scheduled = 1, //sefer planlandı, bilet satışına açık
        Boarding = 2, //yolcu alımı başladı (sefer saati yaklaştı)
        Ongoing = 3, //otobüs şu an yolsa
        Completed = 4, //sefer başarıyla bitti
        Cancelled = 0, //sefer iptal edildi
        Suspended = 5 //sefer geçici olarka durduruldu (satışa kapalı)
    }
}
