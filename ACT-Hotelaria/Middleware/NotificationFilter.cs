using System.Net;
using ACT_Hotelaria.Domain.DomainNotification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ACT_Hotelaria.Middleware;

public class NotificationFilter : IAsyncResultFilter
{
    private readonly NotificationContext _notificationContext;

    public NotificationFilter(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_notificationContext.HasNotifications)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json";

            var response = new 
            {
                success = false,
                statusCode = 400,
                errors = _notificationContext.Notifications.Select(n => new 
                {
                    field = n.Key, 
                    message = n.Message 
                })
            };

            context.Result = new BadRequestObjectResult(response);
        }

        await next();
    }
}