using ACT_Hotelaria.ApiResponse;
using ACT_Hotelaria.Application.UseCase.IdentityUser;
using ACT_Hotelaria.Application.UseCase.IdentityUser.Login;
using ACT_Hotelaria.Application.UseCase.IdentityUser.Refresh;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public sealed class AuthController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponse<UserIdentityRegisterUseCaseResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> RegisterUser(UserIdentityrRegisterRequest register)
    {
        var result = await _mediator.Send(register);
        var response = ApiResponse<UserIdentityRegisterUseCaseResponse>.SuccesResponse(result, 201);
        return Created(string.Empty, response);
    }
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<UserIdentityLoginUseCaseResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult>LoginUser(UserIdentityLoginUseCaseRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request);
        var response = ApiResponse<UserIdentityLoginUseCaseResponse>.SuccesResponse(result, 200);
        return Ok(response);
    }
    
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<UserIdentityLoginUseCaseResponse>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken(RefreshTokenUseCaseRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}