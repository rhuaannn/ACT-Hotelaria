using System.Text.Json;
using ACT_Hotelaria.ApiResponse;
using ACT_Hotelaria.Domain.Interface;

namespace ACT_Hotelaria.Middleware;

public class NotificationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<NotificationMiddleware> _logger;

    public NotificationMiddleware(RequestDelegate next, ILogger<NotificationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        await using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        await _next(context);

        var notifier = context.RequestServices.GetService<INotification>();
        if (notifier != null && notifier.HasValidNotication())
        {
            var errors = notifier.GetNotification().Select(n => n.Message ?? string.Empty).ToList();
            var response = ApiResponse<object>.ErrorResponse(errors, 400);
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
            _logger.LogDebug("NotificationMiddleware intercepted response and returned aggregated notifications.");
            return;
        }
        memoryStream.Seek(0, SeekOrigin.Begin);
        await memoryStream.CopyToAsync(originalBodyStream);
        context.Response.Body = originalBodyStream;
    }
}

