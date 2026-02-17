using FluentValidation;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser.Refresh;

public class RefreshTokenUseCaseValidator : AbstractValidator<RefreshTokenUseCaseRequest>
{
    public RefreshTokenUseCaseValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
    
}