using ACT_Hotelaria.Application.UseCase.Room;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class RoomController : BaseController
{
    private readonly RegisterRoomUseCase _registerRoomUseCase;

    public RoomController(RegisterRoomUseCase registerRoomUseCase)
    {
        _registerRoomUseCase = registerRoomUseCase;
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterRoom([FromBody] RegisterRoomUseCaseRequest request)
    {
        return Ok(await _registerRoomUseCase.Handle(request));
    }
}