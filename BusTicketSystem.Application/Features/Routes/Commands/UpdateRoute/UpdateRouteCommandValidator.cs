using FluentValidation;

namespace BusTicketSystem.Application.Features.Routes.Commands.UpdateRoute
{
    public class UpdateRouteCommandValidator : AbstractValidator<UpdateRouteCommand>
    {
        public UpdateRouteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.DepartureCity).NotEmpty();
            RuleFor(x => x.ArrivalCity).NotEmpty();
            RuleFor(x => x.EstimatedDurationMinutes).GreaterThan(0);
        }
    }
}
