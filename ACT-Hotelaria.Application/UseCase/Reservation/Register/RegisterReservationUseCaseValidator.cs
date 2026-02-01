using ACT_Hotelaria.Message;
using FluentValidation;

namespace ACT_Hotelaria.Application.UseCase.Reservation;

public class RegisterReservationUseCaseValidator : AbstractValidator<RegisterReservationUseCaseRequest>
{
    public RegisterReservationUseCaseValidator()
    {
        RuleFor(r => r.ClientId).NotEmpty().
            WithMessage(ResourceMessages.ClienteObrigatorio);
        
        RuleFor(r => r.CheckIn).NotEmpty()
            .WithMessage(ResourceMessages.CheckinObrigatorio)
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage(ResourceMessages.CheckinObrigatorio);
        
        RuleFor(r => r.CheckOut)
            .NotEmpty()
            .GreaterThan(x => x.CheckIn)
            .WithMessage(ResourceMessages.CheckinAndCheckoutDiferente);        
    }
}