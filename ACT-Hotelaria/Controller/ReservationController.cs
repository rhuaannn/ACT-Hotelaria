using ACT_Hotelaria.Application.UseCase.Reservation;
using ACT_Hotelaria.Application.UseCase.Reservation.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using INotification = ACT_Hotelaria.Domain.Interface.INotification;

namespace ACT_Hotelaria.Controller;

public sealed class ReservationController(IMediator mediator, INotification notification) : BaseController(mediator, notification)
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<RegisterReservationUseCaseResponse>),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterReservation([FromBody] RegisterReservationUseCaseRequest request)
    {
        var result = await _mediator.Send(request);
        var response = ACT_Hotelaria.ApiResponse.ApiResponse<RegisterReservationUseCaseResponse>.SuccesResponse(result, 200);
        
        return Created("",response);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<RegisterReservationUseCaseResponse>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllReservation(CancellationToken cancellationToken = default)
    {
        var query = new GetAllQueryReservation();   
        var result = await _mediator.Send(query);
        var response = ACT_Hotelaria.ApiResponse.ApiResponse<IEnumerable<GetAllReservationUseCaseResponse>>.SuccesResponse(result, 200);
        return Ok(response);
        
    }
    
}