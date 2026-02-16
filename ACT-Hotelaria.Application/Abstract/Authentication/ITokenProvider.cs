namespace ACT_Hotelaria.Application.Abstract.Authentication;

public interface ITokenProvider
{
    public string GetToken();
}