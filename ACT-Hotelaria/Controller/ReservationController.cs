using ACT_Hotelaria.Application.UseCase.Reservation;
using ACT_Hotelaria.Application.UseCase.Reservation.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class ReservationController : BaseController
{
    private readonly RegisterReservationUseCase _registerReservationUseCase;
    private readonly GetAllReservationUseCase _getAllReservationUseCase;

    public ReservationController(RegisterReservationUseCase registerReservationUseCase, GetAllReservationUseCase getAllReservationUseCase)
    {
        _registerReservationUseCase = registerReservationUseCase;
        _getAllReservationUseCase = getAllReservationUseCase;
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterReservation([FromBody] RegisterReservationUseCaseRequest request)
    {
        var response = await _registerReservationUseCase.Handle(request);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReservation()
    {
        var response = await _getAllReservationUseCase.Handle();
        return Ok(response);       
    }
}