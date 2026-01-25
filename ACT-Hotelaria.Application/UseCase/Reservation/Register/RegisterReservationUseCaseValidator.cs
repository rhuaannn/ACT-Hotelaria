using FluentValidation;

namespace ACT_Hotelaria.Application.UseCase.Reservation;

public class RegisterReservationUseCaseValidator : AbstractValidator<Domain.Entities.Reservation>
{
    public RegisterReservationUseCaseValidator()
    {
        RuleFor(r => r.ClientId).NotEmpty().
            WithMessage("Cliente obrigatório!");
        
        RuleFor(r => r.Checkin).NotEmpty()
            .WithMessage("Data de checkin obrigatório!")
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("O check-in deve ser para hoje ou data futura.");
        
        RuleFor(r => r.Checkout)
            .NotEmpty()
            .GreaterThan(x => x.Checkin)
            .WithMessage("A data de check-out deve ser posterior ao check-in.");        
    }
}