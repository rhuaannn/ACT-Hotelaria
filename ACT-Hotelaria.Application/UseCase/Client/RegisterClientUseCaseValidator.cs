using FluentValidation;

namespace ACT_Hotelaria.Application.UseCase.Client;

public class RegisterClientUseCaseValidator : AbstractValidator<RegisterClientUseCaseRequest>
{
    public RegisterClientUseCaseValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome obrigatório!");
        RuleFor(x => x.CPF).NotEmpty().WithMessage("Cpf obrigatório!");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email obrigatório!");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefone obrigatório!");
    }
    
}