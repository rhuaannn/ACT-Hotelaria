using ACT_Hotelaria.Message;
using FluentValidation;

namespace ACT_Hotelaria.Application.UseCase.Product;

public class RegisterProductUseCaseValidator : AbstractValidator<RegisterProductUseCaseRequest>
{
    public RegisterProductUseCaseValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage(ResourceMessages.NomeObrigatorio);
        RuleFor(p => p.Quantity).GreaterThan(0).WithMessage(ResourceMessages.PrecoMaiorQueZero);
        RuleFor(p => p.Value).GreaterThan(0).WithMessage(ResourceMessages.PrecoMaiorQueZero);       
    }
}