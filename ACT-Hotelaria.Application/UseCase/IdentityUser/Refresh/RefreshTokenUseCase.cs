using ACT_Hotelaria.Application.Abstract.Authentication;
using ACT_Hotelaria.Application.Settings;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser.Refresh;

public class RefreshTokenUseCase : IRequestHandler<RefreshTokenUseCaseRequest, RefreshTokenUseCaseResponse>
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IAuthenticationServices _authenticationServices;
    public RefreshTokenUseCase(UserManager<ApplicationUser> userManager,
                                IOptions<JwtSettings> jwtSettings
                                ,IAuthenticationServices authenticationServices)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _authenticationServices = authenticationServices;
    }
    
    public async Task<RefreshTokenUseCaseResponse> Handle(RefreshTokenUseCaseRequest request, CancellationToken cancellationToken)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == request.RefreshToken);
        if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new DomainException("Refresh Token inválido ou expirado.");
        }
        var newAccessToken = _authenticationServices.GenerateToken(user);
        var newRefreshToken = _authenticationServices.RefreshTokenGenerate();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
        
        await _userManager.UpdateAsync(user);
        
        return new RefreshTokenUseCaseResponse
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}