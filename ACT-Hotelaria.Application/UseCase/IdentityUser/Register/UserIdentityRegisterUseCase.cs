using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser;

public class UserIdentityRegisterUseCase :IRequestHandler<UserIdentityrRegisterRequest, UserIdentityRegisterUseCaseResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserIdentityRegisterUseCase(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<UserIdentityRegisterUseCaseResponse> Handle(UserIdentityrRegisterRequest request, CancellationToken cancellationToken)
    {
        var user =  new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };
         var result = await _userManager.CreateAsync(user, request.Password);
         if (!result.Succeeded)
         {
             throw new DomainException($"Falha ao criar usuário");
         }
        return new UserIdentityRegisterUseCaseResponse
        {
            Email = request.Email
        };
    }
}