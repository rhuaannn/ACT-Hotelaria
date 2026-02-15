using FluentValidation;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser.Login;

public class UserIdentityLoginUseCaseValidator :AbstractValidator<UserIdentityrRegisterRequest>
{
    public UserIdentityLoginUseCaseValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("E-mail obrigatório!");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Senha obrigatório!");
    }
    
}