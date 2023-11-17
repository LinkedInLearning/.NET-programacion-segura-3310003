namespace Wpm.Web.Middleware;

public class ErrorLoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger logger;

    public ErrorLoggingMiddleware(RequestDelegate next, 
                ILogger<ErrorLoggingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        finally
        {
            // Log the details of the response after the request has been handled
            logger.LogInformation("Request {Method} {Url} => {StatusCode}", context.Request?.Method, context.Request?.Path.Value, context.Response?.StatusCode);
        }
    }
}
