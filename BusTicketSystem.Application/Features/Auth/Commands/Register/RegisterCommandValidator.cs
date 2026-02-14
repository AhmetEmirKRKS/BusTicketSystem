using FluentValidation;

namespace BusTicketSystem.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş olamaz")
                .MaximumLength(50).WithMessage("Ad 50 karakterden uzun olamaz");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakterden olmalıdır");
        }
    }
}
