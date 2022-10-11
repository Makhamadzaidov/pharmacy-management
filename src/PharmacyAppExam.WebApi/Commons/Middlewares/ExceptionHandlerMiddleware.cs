using Newtonsoft.Json;
using PharmacyAppExam.WebApi.Commons.Exceptions;
using System.Net;
using Telegram.Bot;

namespace PharmacyAppExam.WebApi.Commons.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITelegramBotClient _botClient;
        private const long ChatId = 1781737404;

        public ExceptionHandlerMiddleware(RequestDelegate next, ITelegramBotClient botClient)
        {
            _next = next;
            _botClient = botClient;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (StatusCodeException statusCodeException)
            {
                await HandlerAsync(statusCodeException, httpContext);
            }
            catch (Exception exception)
            {
                await HandlerOtherAsync(exception, httpContext);
            }
        }
        public async Task HandlerAsync(StatusCodeException statusCodeException, HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int)statusCodeException.StatusCode;
            httpContext.Response.ContentType = "application/json";
            string json = JsonConvert.SerializeObject( 
                new { StatusCode = statusCodeException.StatusCode, Message = statusCodeException.Message });

            await _botClient.SendTextMessageAsync(ChatId, statusCodeException.Message);
            await httpContext.Response.WriteAsync(json);
        }
        public async Task HandlerOtherAsync(Exception exception, HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType= "application/json";
            string json = JsonConvert.SerializeObject(
                new { StatusCode = HttpStatusCode.InternalServerError, Message = exception.Message });

            await _botClient.SendTextMessageAsync(ChatId, exception.Message);
            await httpContext.Response.WriteAsync(json);
        }
    }
}
