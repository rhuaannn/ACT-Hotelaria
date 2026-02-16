using ACT_Hotelaria.Application.UseCase.Room;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class RoomController(IMediator mediator) : BaseController(mediator)
{
    private readonly RegisterRoomUseCase _registerRoomUseCase;
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<RegisterRoomUseCaseResponse>),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterRoom([FromBody] RegisterRoomUseCaseRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request);
        var response = ACT_Hotelaria.ApiResponse.ApiResponse<RegisterRoomUseCaseResponse>.SuccesResponse(result, 200);
        return Created(string.Empty,response);
    }
}