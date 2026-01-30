using ACT_Hotelaria.Message;
using FluentValidation;

namespace ACT_Hotelaria.Application.UseCase.Reservation;

public class RegisterReservationUseCaseValidator : AbstractValidator<Domain.Entities.Reservation>
{
    public RegisterReservationUseCaseValidator()
    {
        RuleFor(r => r.ClientId).NotEmpty().
            WithMessage(ResourceMessages.ClienteObrigatorio);
        
        RuleFor(r => r.Checkin).NotEmpty()
            .WithMessage(ResourceMessages.CheckinObrigatorio)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage(ResourceMessages.CheckinObrigatorio);
        
        RuleFor(r => r.Checkout)
            .NotEmpty()
            .GreaterThan(x => x.Checkin)
            .WithMessage(ResourceMessages.CheckinAndCheckoutDiferente);        
    }
}