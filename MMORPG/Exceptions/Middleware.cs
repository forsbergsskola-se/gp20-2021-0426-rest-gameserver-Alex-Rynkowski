using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
            }
        }
    }

    public static class MiddlewareExtensions{
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder){
            return builder.UseMiddleware<Middleware>();
        }
    }
}