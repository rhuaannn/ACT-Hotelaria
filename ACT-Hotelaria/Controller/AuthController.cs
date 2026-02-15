using ACT_Hotelaria.Application.UseCase.IdentityUser;
using ACT_Hotelaria.Application.UseCase.IdentityUser.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class AuthController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<UserIdentityRegisterUseCaseResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>), StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> RegisterUser(UserIdentityrRegisterRequest register)
    {
        var result = await _mediator.Send(register);
        var response = ACT_Hotelaria.ApiResponse.ApiResponse<UserIdentityRegisterUseCaseResponse>.SuccesResponse(result, 201);
        return Created(string.Empty, response);
    }
    [HttpPost("login")]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<UserIdentityLoginUseCaseResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult>LoginUser(UserIdentityLoginUseCaseRequest request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request));
    }
}