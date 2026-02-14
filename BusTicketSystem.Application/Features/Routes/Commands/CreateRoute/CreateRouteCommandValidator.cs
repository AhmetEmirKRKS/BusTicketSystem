using FluentValidation;

namespace BusTicketSystem.Application.Features.Routes.Commands.CreateRoute
{
    public class CreateRouteCommandValidator : AbstractValidator<CreateRouteCommand>
    {
        public CreateRouteCommandValidator()
        {
            RuleFor(x => x.DepartureCity).NotEmpty().WithMessage("Kalkış yeri boş olamaz");
            RuleFor(x => x.ArrivalCity).NotEmpty().WithMessage("Varış yeri boş olamaz");
            RuleFor(x => x.DepartureCity)
                .NotEqual(x => x.ArrivalCity).WithMessage("Kalkış ve varış yeri aynı olamaz");
            RuleFor(x => x.EstimatedDurationMinutes).GreaterThan(0).WithMessage("Süre 0'dan büyük olmalıdır");
        }
    }
}
