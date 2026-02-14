using FluentValidation;

namespace BusTicketSystem.Application.Features.Buses.Commands.CreateBus
{
    public class CreateBusCommandValidator : AbstractValidator<CreateBusCommand>
    {
        public CreateBusCommandValidator()
        {
            RuleFor(x => x.PlateNumber)
                .NotEmpty().WithMessage("{PropertyName} alanı boş geçilemez")
                .MaximumLength(20).WithMessage("{PropertyName} 20 karakterden uzun olamaz");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Otobüs modeli belirtilmelidir")
                .MinimumLength(3).WithMessage("Model adı en az 3 karakter olmalıdır");

            RuleFor(x => x.TotalSeats)
                .GreaterThan(0).WithMessage("Koltuk sayısı 0'dan büyük olmalıdır")
                .LessThanOrEqualTo(100).WithMessage("Bir otobüs en fazla 100 kişilik olabilir");

            RuleFor(x => x.SeatLayout)
                .IsInEnum().WithMessage("Geçersiz koltuk düzeni seçimi");
        }
    }
}
