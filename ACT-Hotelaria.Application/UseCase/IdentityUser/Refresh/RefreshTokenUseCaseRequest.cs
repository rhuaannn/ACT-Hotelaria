using MediatR;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser.Refresh;

public class RefreshTokenUseCaseRequest : IRequest<RefreshTokenUseCaseResponse>
{
    public string RefreshToken { get; set; }
}