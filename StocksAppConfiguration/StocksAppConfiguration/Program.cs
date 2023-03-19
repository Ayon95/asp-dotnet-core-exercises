using StocksAppConfiguration.Options;
using StocksAppConfiguration.ServiceContracts;
using StocksAppConfiguration.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
