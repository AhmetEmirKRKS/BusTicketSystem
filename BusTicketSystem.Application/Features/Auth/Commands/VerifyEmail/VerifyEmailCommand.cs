using MediatR;

namespace BusTicketSystem.Application.Features.Auth.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest<string>
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
