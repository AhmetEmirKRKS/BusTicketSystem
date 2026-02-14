using System.Threading.Tasks;

namespace BusTicketSystem.Application.Interfaces.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
