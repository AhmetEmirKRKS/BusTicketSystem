using MediatR;
using BusTicketSystem.Application.DTOs;

namespace BusTicketSystem.Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<AuthResponse>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
