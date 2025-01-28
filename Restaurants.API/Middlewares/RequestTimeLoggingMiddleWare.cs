
using System.Diagnostics;

namespace Restaurants.API.Middlewares
{
    public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var timer = new Stopwatch();
            timer.Start();
            await next.Invoke(context);

            timer.Stop();
            logger.LogInformation("Time elasped: {time}ms", timer.Elapsed.TotalMilliseconds);
        }
    }
}
