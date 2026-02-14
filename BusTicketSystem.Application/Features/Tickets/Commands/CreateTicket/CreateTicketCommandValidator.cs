using FluentValidation;

namespace BusTicketSystem.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketCommandValidator()
        {
            RuleFor(x => x.TripId).NotEmpty();
            RuleFor(x => x.SeatNumber).GreaterThan(0).WithMessage("Geçersiz koltuk numarası");
            RuleFor(x => x.PassengerFirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.PassengerLastName).NotEmpty().MaximumLength(50);

            RuleFor(x => x.PassengerNationalId)
                .NotEmpty()
                .Length(11).WithMessage("TC kimlik No 11 haneli olmalıdır")
                .Matches("^[0-9]*$").WithMessage("TC Kimlik No sadece rakamlardan oluşmalıdır");

            RuleFor(x => x.PassengerPhone).NotEmpty();
            RuleFor(x => x.PassengerGender).IsInEnum().WithMessage("Cinsiyet seçimi zorunludur");
        }
    }
}
