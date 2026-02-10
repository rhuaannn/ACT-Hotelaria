using FluentValidation;

namespace ACT_Hotelaria.Application.UseCase.Room;

public class RegisterRoomUseCaseValidator : AbstractValidator<RegisterRoomUseCaseRequest>
{
    public RegisterRoomUseCaseValidator()
    {
        RuleFor(x => x.TypeRoom).NotEmpty().IsInEnum().WithMessage("Tipo de quarto obrigatório");
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantidade de quartos deve ser maior que zero");
    }
    
}