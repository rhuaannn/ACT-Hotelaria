using ACT_Hotelaria.Message;
using FluentValidation;

namespace ACT_Hotelaria.Application.UseCase.Consumption;

public class RegisterConsumptionUseCaseValidator : AbstractValidator<RegisterConsumptionUseCaseRequest>
{
    public RegisterConsumptionUseCaseValidator()
    {
        RuleFor(c => c.ProductId).NotEmpty().WithMessage("ProdutoId Obrigatório");
        RuleFor(c => c.Quantity).GreaterThanOrEqualTo(1).WithMessage(ResourceMessages.PrecoMaiorQueZero);
        RuleFor(c => c.ReservationId).NotEmpty().WithMessage("ReservaId Obrigatória");
    }
}