using MediatR;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser.Login;

public class UserIdentityLoginUseCaseRequest : IRequest<UserIdentityLoginUseCaseResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}