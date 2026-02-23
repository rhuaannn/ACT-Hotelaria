using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Model;
using ACT_Hotelaria.Domain.Notification;
using ACT_Hotelaria.Message;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using INotification = ACT_Hotelaria.Domain.Interface.INotification;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser;

public class UserIdentityRegisterUseCase :IRequestHandler<UserIdentityrRegisterRequest, UserIdentityRegisterUseCaseResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<UserIdentityRegisterUseCase> _logger;
    private readonly INotification _notification;

    public UserIdentityRegisterUseCase(UserManager<ApplicationUser> userManager, ILogger<UserIdentityRegisterUseCase> logger, INotification notification)
    {
        _userManager = userManager;
        _logger = logger;
        _notification = notification;
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
             _logger.LogWarning("Falha ao criar usuário {Email}. Erros: {Errors}", request.Email, string.Join(", ", result.Errors.Select(e => e.Description))); 
             foreach (var error in result.Errors)
             {
                 _notification.Handle(new Notification(error.Description));
             }
             return default;
         }

         _logger.LogInformation("Usuário criado com sucesso!");
        return new UserIdentityRegisterUseCaseResponse
        {
            Email = request.Email
        };
    }
}