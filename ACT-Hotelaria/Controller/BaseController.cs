using ACT_Hotelaria.ApiResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using INotification = ACT_Hotelaria.Domain.Interface.INotification;

namespace ACT_Hotelaria.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseController(IMediator mediator, INotification notification) : ControllerBase
{
    private readonly INotification _notification = notification;
    protected IMediator _mediator = mediator;
    
    protected ActionResult<ApiResponse<T>> CustomResponse<T>(T result = default)
    {
        if (_notification.HasValidNotication())
        {
            var erros = _notification.GetNotification().Select(n => n.Message).ToList();
            return BadRequest(ApiResponse<T>.ErrorResponse(erros, 400));
        }
        return Ok(ApiResponse<T>.SuccesResponse(result, 200));
    }
}