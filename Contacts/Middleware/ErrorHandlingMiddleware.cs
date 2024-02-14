using System.Net;
using System.Security.Authentication;

namespace Contacts.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!context.Response.HasStarted)
                    await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        public Task HandleException(HttpContext context, Exception ex)
        {
            if (context.Response.HasStarted) return Task.CompletedTask;

            var code = HttpStatusCode.InternalServerError;
            string? result = null;
            switch (ex)
            {
                case HttpRequestException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case InvalidCredentialException:
                    code = HttpStatusCode.Forbidden;
                    break;
                case BadHttpRequestException:
                    code = HttpStatusCode.BadRequest; break;
            }

            context.Response.ContentType = "application/text";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(ex.Message);
        }
    }
}
