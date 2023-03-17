// This is the old way (implementing IMiddleware) of writing custom middleware classes

namespace MyFirstWebApp.CustomMiddleware
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("<p>From my custom middleware class.</p>");
            await next(context);
            await context.Response.WriteAsync("<p>End of my custom middleware class.</p>");
        }
    }
    // Extension method used to add the middleware to the request pipeline
    public static class MyCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
