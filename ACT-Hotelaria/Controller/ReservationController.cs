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
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<RegisterReservationUseCaseResponse>),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterReservation([FromBody] RegisterReservationUseCaseRequest request)
    {
        var result = await _registerReservationUseCase.Handle(request);
        var response = ACT_Hotelaria.ApiResponse.ApiResponse<RegisterReservationUseCaseResponse>.SuccesResponse(result);
        
        return Created("",response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<RegisterReservationUseCaseResponse>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>),StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetAllReservation()
    {
        var result = await _getAllReservationUseCase.Handle();
        var response = ACT_Hotelaria.ApiResponse.ApiResponse<IEnumerable<GetAllReservationUseCaseResponse>>
            .SuccesResponse(result);
        return Ok(response);       
    }
}