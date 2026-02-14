using FluentValidation;

namespace BusTicketSystem.Application.Features.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("E-posta adresi gereklidir")
                .EmailAddress().WithMessage("Geçerli bir e-posta formatı giriniz");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Lütfen şifre giriniz");
        }
    }
}
