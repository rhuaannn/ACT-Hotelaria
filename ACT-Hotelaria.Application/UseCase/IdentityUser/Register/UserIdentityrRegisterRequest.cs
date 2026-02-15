using MediatR;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser;

public class UserIdentityrRegisterRequest : IRequest<UserIdentityRegisterUseCaseResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    
}