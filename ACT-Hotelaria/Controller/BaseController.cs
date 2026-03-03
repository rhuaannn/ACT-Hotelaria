using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACT_Hotelaria.ApiResponse;
using INotification = ACT_Hotelaria.Domain.Interface.INotification;

namespace ACT_Hotelaria.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class BaseController(IMediator mediator, INotification notification) : ControllerBase
{
    protected IMediator _mediator = mediator;
    protected INotification _notification = notification;

    protected IActionResult CustomResponse<T>(T result)
    {
        if (_notification.HasValidNotication())
        {
            var errors = _notification.GetNotification().Select(n => n.Message ?? string.Empty).ToList();
            // debug log to inspect collected notifications
            Console.WriteLine($"[DEBUG] Notifications count: {errors.Count}; messages: {string.Join(" | ", errors)}");
            var errorResponse = ApiResponse<object>.ErrorResponse(errors, 400);
            return BadRequest(errorResponse);
        }

        var successResponse = ApiResponse<T>.SuccesResponse(result, 200);
        return Ok(successResponse);
    }

    protected IActionResult CustomResponse()
    {
        if (_notification.HasValidNotication())
        {
            var errors = _notification.GetNotification().Select(n => n.Message ?? string.Empty).ToList();
            // debug log to inspect collected notifications
            Console.WriteLine($"[DEBUG] Notifications count: {errors.Count}; messages: {string.Join(" | ", errors)}");
            var errorResponse = ApiResponse<object>.ErrorResponse(errors, 400);
            return BadRequest(errorResponse);
        }

        var successResp = ApiResponse<object>.SuccesResponse(null!, 200);
        return Ok(successResp);
    }

    protected void NotifyModelStateErrors()
    {
        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        foreach (var error in errors)
        {
            _notification.Handle(new ACT_Hotelaria.Domain.Notification.Notification(error));
        }
    }
}