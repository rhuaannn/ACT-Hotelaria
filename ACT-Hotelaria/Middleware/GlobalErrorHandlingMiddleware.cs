using System.Net;
using System.Text.Json;
using ACT_Hotelaria.Domain.Exception;

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
            // Passa a requisição para frente
            await _next(context);
        }
        catch (Exception ex)
        {
            // Se algo explodir em qualquer lugar depois daqui, cai aqui
            _logger.LogError(ex, "Ocorreu um erro não tratado na aplicação.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Defina o status code padrão (500 - Erro interno)
        var statusCode = (int)HttpStatusCode.InternalServerError;
        var message = "Ocorreu um erro interno no servidor.";
        
        // Aqui você pode personalizar baseado no tipo de erro (muito útil para DDD)
        switch (exception)
        {
            // Exemplo: Se for uma exceção de domínio (validação), retorna 400
            case ArgumentException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = exception.Message; // Mostra a mensagem real do erro de validação
                break;

            case BaseException baseException:
                statusCode = (int)baseException.StatusCode;
                message = baseException.Message;
                break;

            case KeyNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                message = "Recurso não encontrado.";
                break;
        }

        context.Response.StatusCode = statusCode;

        // CRUCIAL: Aqui usamos o SEU formato de resposta para manter consistência
        // Supondo que seu ApiResponse tenha propriedades como Success, Message, Data
        var responseModel = new 
        {
            Success = false,
            Message = message,
            Errors = new[] { exception.Message } // Opcional: detalhes técnicos (cuidado em produção)
        };

        var jsonResponse = JsonSerializer.Serialize(responseModel);
        
        return context.Response.WriteAsync(jsonResponse);
    }
}
