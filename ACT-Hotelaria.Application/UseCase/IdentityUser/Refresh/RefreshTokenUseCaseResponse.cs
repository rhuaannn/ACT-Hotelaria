using MediatR;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser.Refresh;

public class RefreshTokenUseCaseResponse : IRequest<RefreshTokenUseCaseResponse>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}