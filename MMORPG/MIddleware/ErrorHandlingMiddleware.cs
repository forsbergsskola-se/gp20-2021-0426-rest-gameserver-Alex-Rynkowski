using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MMORPG.Exceptions{
    public class ErrorHandlingMiddleware{
        readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next){
            this.next = next;
        }

        public async Task Invoke(HttpContext context){
            try{
                await this.next.Invoke(context);
            }
            catch (Exception e){
                var response = context.Response;

                response.ContentType = "application.json";
                
                response.StatusCode = e switch{
                    NotFoundException => (int) HttpStatusCode.NotFound,
                    NoQuestFoundException => (int) HttpStatusCode.NotFound,
                    PlayerException => (int) HttpStatusCode.NotFound
                };
                var result = JsonSerializer.Serialize(new{message = e.Message});
                await response.WriteAsync(result);
            }
        }
    }

    public static class MiddlewareExtensions{
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder){
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}