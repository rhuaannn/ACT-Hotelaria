using Microsoft.AspNetCore.Identity;

namespace ACT_Hotelaria.Domain.Model;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}