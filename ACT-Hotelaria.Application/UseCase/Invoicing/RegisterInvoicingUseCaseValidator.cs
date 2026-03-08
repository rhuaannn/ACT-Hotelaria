using FluentValidation;

namespace ACT_Hotelaria.Application.UseCase.Invoicing;

public class RegisterInvoicingUseCaseValidator : AbstractValidator<RegisterInvoicingUseCaseRequest>
{
    public RegisterInvoicingUseCaseValidator()
    {
        RuleFor(x => x.ReservationId).NotEmpty();
    }
    
}