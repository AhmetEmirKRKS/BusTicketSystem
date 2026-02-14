using FluentValidation;

namespace BusTicketSystem.Application.Features.Buses.Commands.UpdateBus
{
    public class UpdateBusCommandValidator : AbstractValidator<UpdateBusCommand>
    {
        public UpdateBusCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.PlateNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Model).NotEmpty().MinimumLength(3);
            RuleFor(x => x.TotalSeats).GreaterThan(0);
            RuleFor(x => x.SeatLayout).IsInEnum();
        }
    }
}
