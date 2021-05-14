using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MMORPG.Exceptions{
    public class Middleware{
        readonly RequestDelegate next;

        public Middleware(RequestDelegate next){
            this.next = next;
        }

        public async Task Invoke(HttpContext context){
            try{
                await this.next.Invoke(context);
            }
            catch (Exception e){
                var response = context.Response;

                response.StatusCode = e switch{
                    NotFoundException exception => (int) HttpStatusCode.NotFound,
                    NoQuestFoundException exception => (int) HttpStatusCode.NotFound
                };
                var result = JsonSerializer.Serialize(new{message = e?.Message});
                await response.WriteAsync(result);
            }
        }
    }

    public static class MiddlewareExtensions{
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder){
            return builder.UseMiddleware<Middleware>();
        }
    }
}