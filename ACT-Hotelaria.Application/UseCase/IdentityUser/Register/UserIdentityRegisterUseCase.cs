using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Model;
using ACT_Hotelaria.Message;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ACT_Hotelaria.Application.UseCase.IdentityUser;

public class UserIdentityRegisterUseCase :IRequestHandler<UserIdentityrRegisterRequest, UserIdentityRegisterUseCaseResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<UserIdentityRegisterUseCase> _logger;
    public UserIdentityRegisterUseCase(UserManager<ApplicationUser> userManager, ILogger<UserIdentityRegisterUseCase> logger)
    {
        _userManager = userManager;
        _logger = logger;
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
             throw new InternalException(ResourceMessages.ErroInesperado);
         }

         _logger.LogInformation("Usuário criado com sucesso!");
        return new UserIdentityRegisterUseCaseResponse
        {
            Email = request.Email
        };
    }
}