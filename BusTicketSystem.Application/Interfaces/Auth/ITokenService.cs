using BusTicketSystem.Domain.Entities;

namespace BusTicketSystem.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
