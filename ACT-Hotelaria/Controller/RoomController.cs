using ACT_Hotelaria.ApiResponse;
using ACT_Hotelaria.Application.UseCase.Room;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public sealed class RoomController(IMediator mediator) : BaseController(mediator)
{
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<RegisterRoomUseCaseResponse>),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterRoom([FromBody] RegisterRoomUseCaseRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request);
        var response = ApiResponse<RegisterRoomUseCaseResponse>.SuccesResponse(result, 200);
        return Created(string.Empty,response);
    }
}