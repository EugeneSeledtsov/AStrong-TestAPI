namespace Exceptions
{
    using Microsoft.AspNetCore.Http;
    using System.Net;

    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var (status, message) = GetResponse(exception);
                response.StatusCode = (int)status;
                await response.WriteAsync(message);
            }
        }
        private static (HttpStatusCode code, string message) GetResponse(Exception exception)
        {
            var code = exception switch
            {
                KeyNotFoundException or EntityNotFoundException => HttpStatusCode.NotFound,

                EntityAlreadyExistsException => HttpStatusCode.Conflict,

                BaseAppException => HttpStatusCode.BadRequest,

                _ => HttpStatusCode.InternalServerError,
            };
            return (code, exception.Message);
        }
    }
}
