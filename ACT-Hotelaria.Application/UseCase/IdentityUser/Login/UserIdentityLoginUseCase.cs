using ACT_Hotelaria.Application.Abstract.Authentication;
using ACT_Hotelaria.Application.Settings;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Model;
using ACT_Hotelaria.Domain.Notification;
using ACT_Hotelaria.Message;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using INotification = ACT_Hotelaria.Domain.Interface.INotification;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser.Login;

public class UserIdentityLoginUseCase : IRequestHandler<UserIdentityLoginUseCaseRequest, UserIdentityLoginUseCaseResponse>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthenticationServices _authenticationServices;
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<UserIdentityLoginUseCase> _logger;
    private readonly INotification _notification;
     
    public UserIdentityLoginUseCase(SignInManager<ApplicationUser> signInManager, IAuthenticationServices authenticationServices
                                    ,UserManager<ApplicationUser> userManager,
                                    ILogger<UserIdentityLoginUseCase> logger,
                                    INotification notification,
                                    IOptions<JwtSettings> jwtSettings)
    {
        _signInManager = signInManager;
        _authenticationServices = authenticationServices;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _logger = logger;
        _notification = notification;
    }
    
    public async Task<UserIdentityLoginUseCaseResponse> Handle(UserIdentityLoginUseCaseRequest request, CancellationToken cancellationToken)
    {
        
        var user = await _userManager.FindByEmailAsync(request.Email);
        var loginUser = await _signInManager.PasswordSignInAsync(request.Email, request.Password,
                                                                        false, true);
           if (!loginUser.Succeeded)
           {
               _logger.LogWarning("Usuário ou senha inválido!");
               _notification.Handle(new Notification("Usuário ou senha inválidos!"));
           }

           if (loginUser.IsLockedOut)
           {
               _logger.LogWarning("Usuário bloqueado!");
               _notification.Handle(new Notification("Usuário bloqueado por tentativas inválidas de credenciaiais"));
           }

           var accessToken = _authenticationServices.GenerateToken(user);
           var refreshToken = _authenticationServices.RefreshTokenGenerate();
           user.RefreshToken = refreshToken;
           user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
           await _userManager.UpdateAsync(user);
           _logger.LogInformation("Usuário logado com sucesso!");
          
           return new UserIdentityLoginUseCaseResponse
           {
               Token = accessToken,
               RefreshToken = refreshToken
           };
    }
}