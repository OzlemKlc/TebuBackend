using System.Text.Json;
using Tebu.API.Exceptions;

namespace Tebu.API.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {

                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string responseString;
            ExceptionResponse response;
            int httpStatusCode;
            if (exception is CustomException customException)
            {
                response = new ExceptionResponse
                {
                    Status = customException.HttpStatusCode,
                    Message = customException.Message
                };
                responseString = JsonSerializer.Serialize(response);
                httpStatusCode = customException.HttpStatusCode;
            }
            else
            {
                response = new ExceptionResponse
                {
                    Status = 500,
                    Message = "Internal Server Error"
                };

                responseString = JsonSerializer.Serialize(response);
                httpStatusCode = 500;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = httpStatusCode;

            await context.Response.WriteAsync(responseString);
        }

        public record ExceptionResponse
        {
            public string Message { get; init; }
            public int Status { get; init; }
        }
    }

    public static class ExceptionHandlerMiddlewareExtentions
    {
        public static IApplicationBuilder UseCustomExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }

    }
}
