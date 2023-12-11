using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger Logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env){
            this.Logger = logger;
            this.env = env;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context){
            try{
                await next(context);
            }
            catch(Exception ex){
                Logger.LogError(ex,ex.Message);

                int statusCode = (int)StatusCodes.Status500InternalServerError;
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var jsonOptions = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var response = env.IsDevelopment() ? 
                                new ApiException(statusCode,ex.Message,ex.StackTrace):
                                new ApiException(statusCode);
                
                var json = JsonSerializer.Serialize(response,jsonOptions);

                await context.Response.WriteAsync(json);
            }
        }

    }
}