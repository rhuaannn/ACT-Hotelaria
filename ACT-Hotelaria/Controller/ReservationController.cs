using ACT_Hotelaria.Application.UseCase.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class ReservationController : BaseController
{
    private readonly RegisterReservationUseCase _registerReservationUseCase;

    public ReservationController(RegisterReservationUseCase registerReservationUseCase)
    {
        _registerReservationUseCase = registerReservationUseCase;
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterReservation([FromBody] RegisterReservationUseCaseRequest request)
    {
        var response = await _registerReservationUseCase.Handle(request);
        return Ok(response);
    }
}