using System;
using System.Net;
using System.Threading.Tasks;
using FB.TransactionalOutbox.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FB.TransactionalOutbox.Infrastructure.Middlewares
{
    public class UserFriendlyExceptionMiddleware
    {
        private readonly ILogger<UserFriendlyExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public UserFriendlyExceptionMiddleware(ILogger<UserFriendlyExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UserFriendlyException ex)
            {
                _logger.LogWarning(ex.Message);
                await HandleUserFriendlyExceptionAsync(context, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                await context.Response.WriteAsync("");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                await HandleUserFriendlyExceptionAsync(context, "Oh omg! An error occured. Please try again later.");
            }
        }

        private Task HandleUserFriendlyExceptionAsync(HttpContext context, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ApiResponse(message),
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }
    }
}