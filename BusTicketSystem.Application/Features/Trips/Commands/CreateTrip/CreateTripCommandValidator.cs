using FluentValidation;
using System;

namespace BusTicketSystem.Application.Features.Trips.Commands.CreateTrip
{
    public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
    {
        public CreateTripCommandValidator()
        {
            RuleFor(x => x.BusId).NotEmpty().WithMessage("Otobüs seçimi zorunludur");
            RuleFor(x => x.RouteId).NotEmpty().WithMessage("Güzergah seçimi zorunludur");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Bilet fiyatı 0'dan büyük olmalıdır");

            RuleFor(x => x.DepartureDate)
                .NotEmpty()
                .GreaterThan(DateTime.UtcNow).WithMessage("Geçmiş bir tarihe sefer düzenlenemez");
        }
    }
}
