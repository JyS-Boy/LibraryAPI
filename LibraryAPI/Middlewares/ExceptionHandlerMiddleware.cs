using Library.Common.Helpers;
using System.Text.Json;

namespace LibraryAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
                _logger.LogError(ex, "Ocurrió un error inesperado: {Message}", ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = Result.Failure("Ocurrió un error inesperado. Contacte con el administrador del sistema.");

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
            return context.Response.WriteAsync(json);
        }
    }
}