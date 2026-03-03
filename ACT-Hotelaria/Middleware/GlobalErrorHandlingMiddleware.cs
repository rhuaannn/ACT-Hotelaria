using System.Net;
using System.Text.Json;
using ACT_Hotelaria.ApiResponse;  
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Message;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

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
                await HandleExceptionAsync(context, ex);
            }

        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var traceId = context!.TraceIdentifier;

            context!.Response.ContentType = "application/json";

            if (exception is ValidationException validationException)
            {
                await HandleValidationExceptionAsync(context, validationException);
                return;
            }

            if (exception is BaseException apiException)
            {
                await HandleApiExceptionAsync(context, apiException);
                return;
            }

            await HandleGenericExceptionAsync(context, exception, traceId);
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var validationErrors = exception.Errors.Select(error => error.ErrorMessage).ToList();
            var response = ApiResponse<object>.ErrorResponse(validationErrors, 400);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var jsonResponse = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(jsonResponse);
        }

        private async Task HandleApiExceptionAsync(HttpContext context, BaseException exception)
        {
            context.Response.StatusCode = (int)exception.StatusCode;

            var response = ApiResponse<object>.ErrorResponse(new List<string> { exception.Message }, (int)exception.StatusCode);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var jsonResponse = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(jsonResponse);
        }

        private async Task HandleGenericExceptionAsync(HttpContext context, Exception exception, string traceId)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Title = ResourceMessages.ErroInesperado,
                Status = context.Response.StatusCode,
                Instance = context.Request.Path,
                Detail = ResourceMessages.ErroInesperado,
            };

            problemDetails.Extensions["traceId"] = traceId;
            
            var jsonResponse = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await context.Response.WriteAsync(jsonResponse);
        }
}
    