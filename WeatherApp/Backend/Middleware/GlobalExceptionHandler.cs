using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is OperationCanceledException)
        {
            if (context.RequestAborted.IsCancellationRequested)
                return true;

            // таймаут
            _logger.LogError(exception.Message);
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            
            var operationCanceledDetails = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Detail = "Сервер WeatherApi не отвечает" 
            };
            
            await context.Response.WriteAsJsonAsync(operationCanceledDetails, cancellationToken);
            return true;
        }

        _logger.LogError(exception.Message);

        context.Response.StatusCode = exception switch
        {
            HttpRequestException {StatusCode: not null} ex=> (int)ex.StatusCode,
            HttpRequestException => StatusCodes.Status503ServiceUnavailable,
            InvalidOperationException => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        var exceptionDetails = new ProblemDetails
        {
            Status = context.Response.StatusCode,
            Detail = exception.Message
        };
        
        await context.Response.WriteAsJsonAsync(exceptionDetails, cancellationToken);
        return true;
    }
}