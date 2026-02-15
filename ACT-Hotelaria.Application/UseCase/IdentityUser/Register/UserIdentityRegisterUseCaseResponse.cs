namespace ACT_Hotelaria.Application.UseCase.IdentityUser;

public class UserIdentityRegisterUseCaseResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; }
}