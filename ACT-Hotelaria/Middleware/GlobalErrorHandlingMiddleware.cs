using System.Net;
using System.Text.Json;
using ACT_Hotelaria.ApiResponse;  
using ACT_Hotelaria.Domain.Exception;
using FluentValidation;

namespace ACT_Hotelaria.Middleware;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

    public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro não tratado na aplicação.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = (int)HttpStatusCode.InternalServerError;
        var message = "Ocorreu um erro interno no servidor.";
        
        switch (exception)
        {
            case ArgumentException: 
                statusCode = (int)HttpStatusCode.BadRequest; 
                message = exception.Message;
                break;

            case KeyNotFoundException: 
                statusCode = (int)HttpStatusCode.NotFound; 
                message = exception.Message;
                break;
            case ValidationException validationException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = validationException.Errors.First().ErrorMessage;
                break;
            case BaseException baseException:
                statusCode = (int)baseException.StatusCode;
                message = baseException.Message;
                break;
        }

        context.Response.StatusCode = statusCode;
        
        var errors = new List<string> { message };
 
        var responseModel = ApiResponse<object>.ErrorResponse(errors, statusCode);

        var jsonOptions = new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        };
        
        var jsonResponse = JsonSerializer.Serialize(responseModel, jsonOptions);
        
        return context.Response.WriteAsync(jsonResponse);
    }
}