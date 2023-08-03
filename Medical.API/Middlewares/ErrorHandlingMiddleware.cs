using Medical.BL.Exceptions;
using System.Net;
using System.Text.Json;

namespace Medical.API.Middlewares
{
    /// <summary>
    /// Global handling middleware to handle exceptions which occurres during runtime
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleAsync(context, ex);
            }
        }

        private static Task HandleAsync(HttpContext context, Exception exception)
        {
            var exceptionResult = JsonSerializer.Serialize(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            switch (exception)
            {
                case EntityNotFoundException:
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    }
                default:
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    }
            }

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
