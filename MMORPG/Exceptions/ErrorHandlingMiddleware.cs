using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MMORPG{
    public class ErrorHandlingMiddleware{
        readonly RequestDelegate _next;
        readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next){
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext){
            try{
                await this._next(httpContext);
            }
            catch (Exception e){
                Console.WriteLine(httpContext.Response.StatusCode);
            }
        }
    }

// Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareExtensions{
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder){
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}