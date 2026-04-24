using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Middleware;

public class GlobalExceptionHandler (ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is OperationCanceledException)
        {
            if (httpContext.RequestAborted.IsCancellationRequested)
                return true;

            // таймаут
            logger.LogError("{Message}", exception.Message);
            httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            
            var operationCanceledDetails = new ProblemDetails
            {
                Status = httpContext.Response.StatusCode,
                Detail = "Сервер WeatherApi не отвечает" 
            };
            
            await httpContext.Response.WriteAsJsonAsync(operationCanceledDetails, cancellationToken);
            return true;
        }

        logger.LogError("{Message}", exception.Message);

        httpContext.Response.StatusCode = exception switch
        {
            HttpRequestException {StatusCode: not null} ex=> (int)ex.StatusCode,
            HttpRequestException => StatusCodes.Status503ServiceUnavailable,
            _ => StatusCodes.Status500InternalServerError
        };

        var exceptionDetails = new ProblemDetails
        {
            Status = httpContext.Response.StatusCode,
            Detail = exception.Message
        };
        
        await httpContext.Response.WriteAsJsonAsync(exceptionDetails, cancellationToken);
        return true;
    }
}