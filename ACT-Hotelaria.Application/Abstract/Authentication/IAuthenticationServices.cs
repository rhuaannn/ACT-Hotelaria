using ACT_Hotelaria.Domain.Model;

namespace ACT_Hotelaria.Application.Abstract.Authentication;

public interface IAuthenticationServices
{
    public string GenerateToken(ApplicationUser username);
    public string RefreshTokenGenerate();
    public bool ValidateToken(string token);
}