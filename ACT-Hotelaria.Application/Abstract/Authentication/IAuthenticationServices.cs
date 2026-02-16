namespace ACT_Hotelaria.Application.Abstract.Authentication;

public interface IAuthenticationServices
{
    public string GenerateToken(string username);
}