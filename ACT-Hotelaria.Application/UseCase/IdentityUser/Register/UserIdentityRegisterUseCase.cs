using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser;

public class UserIdentityRegisterUseCase :IRequestHandler<UserIdentityrRegisterRequest, UserIdentityRegisterUseCaseResponse>
{
    private readonly UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;

    public UserIdentityRegisterUseCase(UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<UserIdentityRegisterUseCaseResponse> Handle(UserIdentityrRegisterRequest request, CancellationToken cancellationToken)
    {
        var user =  new Microsoft.AspNetCore.Identity.IdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };
         await _userManager.CreateAsync(user, request.Password);
        return new UserIdentityRegisterUseCaseResponse
        {
            Email = request.Email
        };
    }
}