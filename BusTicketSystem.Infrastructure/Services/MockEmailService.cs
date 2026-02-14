using BusTicketSystem.Application.Interfaces.Infrastructure;
using System;
using System.Threading.Tasks;

namespace BusTicketSystem.Infrastructure.Services
{
    public class MockEmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"📧 [SAHTE MAIL GÖNDERİLDİ]");
            Console.WriteLine($"Kime: {to}");
            Console.WriteLine($"Konu: {subject}");
            Console.WriteLine($"İçerik: {body}");
            Console.WriteLine("--------------------------------------------------");
            
            return Task.CompletedTask;
        }
    }
}
