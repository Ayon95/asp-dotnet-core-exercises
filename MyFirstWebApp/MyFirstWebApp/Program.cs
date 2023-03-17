using Microsoft.AspNetCore.WebUtilities;
using MyFirstWebApp.CustomMiddleware;

namespace MyFirstWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<MyCustomMiddleware>();
            var app = builder.Build();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // route parameter with a default value
                endpoints.MapGet("/employee/profile/{employeeName=Michael}", async context =>
                {
                    string? employeeName = Convert.ToString(context.Request.RouteValues["employeeName"]);
                    await context.Response.WriteAsync($"Employee - {employeeName}");
                });
            });

            app.Use(async (context, next) =>
            {
                var path = context.Request.Path;
                var method = context.Request.Method;
                var query = context.Request.Query;

                context.Response.Headers["Content-Type"] = "text/html";

                await context.Response.WriteAsync("<h1>Hello World</h1><p>This is the subtitle</p>");
                await context.Response.WriteAsync($"<p>Request URL: {path}</p>");
                await context.Response.WriteAsync($"<p>Request method: {method}</p>");

                if (method == "GET" & query.ContainsKey("id"))
                {
                    await context.Response.WriteAsync($"<p>id: {query["id"]}</p>");
                }

                await context.Response.WriteAsync("<h2>Request headers</h2>");
                foreach (var header in context.Request.Headers)
                {
                    await context.Response.WriteAsync($"<dt>{header.Key}<dt>");
                    await context.Response.WriteAsync($"<dd>{header.Value}<dd>");
                }

                var reader = new StreamReader(context.Request.Body);
                var data = await reader.ReadToEndAsync();

                var queryParams = QueryHelpers.ParseQuery(data);

                if (queryParams.ContainsKey("name"))
                {
                    var name = queryParams["name"][0];
                    await context.Response.WriteAsync($"<p>Name: {name}");
                }

                await next(context);

                await context.Response.WriteAsync("<p>End of middleware 1</p>");
            });

            // app.UseMiddleware<MyCustomMiddleware>();
            app.UseMyCustomMiddleware();

            app.UseGreetPersonMiddleware();

            app.Run(async context =>
            {
                await context.Response.WriteAsync("<p>From middleware 3.</p>");
            });

            app.Run();
        }
    }
}