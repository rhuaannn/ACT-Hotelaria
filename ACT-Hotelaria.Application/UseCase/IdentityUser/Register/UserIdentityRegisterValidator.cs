using FluentValidation;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser;

public class UserIdentityRegisterValidator : AbstractValidator<UserIdentityrRegisterRequest>
{
    public UserIdentityRegisterValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password)
            .WithMessage("Senhas não conferem!"); 
    }
}