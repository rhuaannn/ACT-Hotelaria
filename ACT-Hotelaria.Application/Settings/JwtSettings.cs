namespace ACT_Hotelaria.Application.Settings;

public class JwtSettings
{
    public string SecretKey { get; init; } = string.Empty;
    public int ExpirationInMinutes { get; init; }
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public int RefreshTokenExpirationInDays { get; init; }
}