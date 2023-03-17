// This is the new and preferred way of writing custom middleware classes

namespace MyFirstWebApp.CustomMiddleware
{
    public class GreetPersonMiddleware
    {
        private readonly RequestDelegate _next;

        public GreetPersonMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Query.ContainsKey("name"))
            {
                var name = context.Request.Query["name"];
                await context.Response.WriteAsync($"<p>Hello {name}.</p>");
            }
            await _next(context);
        }
    }

    public static class GreetPersonMiddlewareExtensions
    {
        public static IApplicationBuilder UseGreetPersonMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GreetPersonMiddleware>();
        }
    }
}
