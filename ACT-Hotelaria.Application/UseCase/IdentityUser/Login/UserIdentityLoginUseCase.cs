using ACT_Hotelaria.Domain.Exception;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser.Login;

public class UserIdentityLoginUseCase : IRequestHandler<UserIdentityLoginUseCaseRequest, UserIdentityLoginUseCaseResponse>
{
    private readonly SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> _signInManager;
    
    public UserIdentityLoginUseCase(SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }
    
    public async Task<UserIdentityLoginUseCaseResponse> Handle(UserIdentityLoginUseCaseRequest request, CancellationToken cancellationToken)
    {
       var loginUser = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
       if (!loginUser.Succeeded)
       {
           throw new DomainException("Dados inválidos!");
       }
       return new UserIdentityLoginUseCaseResponse
       {
           Token = request.Email
       };
    }
}