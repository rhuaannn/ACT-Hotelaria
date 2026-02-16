using ACT_Hotelaria.Application.Abstract.Authentication;
using ACT_Hotelaria.Domain.Exception;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser.Login;

public class UserIdentityLoginUseCase : IRequestHandler<UserIdentityLoginUseCaseRequest, UserIdentityLoginUseCaseResponse>
{
    private readonly SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> _signInManager;
    private readonly IAuthenticationServices _authenticationServices;
     
    public UserIdentityLoginUseCase(SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> signInManager, IAuthenticationServices authenticationServices)
    {
        _signInManager = signInManager;
        _authenticationServices = authenticationServices;
    }
    
    public async Task<UserIdentityLoginUseCaseResponse> Handle(UserIdentityLoginUseCaseRequest request, CancellationToken cancellationToken)
    {
       var loginUser = await _signInManager.PasswordSignInAsync(request.Email, request.Password, 
                                                                    false, true);
       if (!loginUser.Succeeded)
       {
           throw new DomainException("Dados inválidos!");
       }

       if (loginUser.IsLockedOut)
       {
           throw new DomainException("Usuário bloqueado por tentativas inválidas de credenciaiais");
       }
        
       var accessToken = _authenticationServices.GenerateToken(request.Email);
      
       return new UserIdentityLoginUseCaseResponse
       {
           Token = accessToken
       };
    }
}