using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksAppConfiguration.Models;
using StocksAppConfiguration.Options;
using StocksAppConfiguration.ServiceContracts;

namespace StocksAppConfiguration.Controllers
{
    public class TradeController : Controller
    {
        private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly IConfiguration _configuration;

        public TradeController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService, IConfiguration configuration)
        {
            _tradingOptions = tradingOptions;
            _finnhubService = finnhubService;
            _configuration = configuration;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            string stockSymbol = _tradingOptions.Value.DefaultStockSymbol!;

            Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(stockSymbol);
            Dictionary<string, object>? stockPriceQuote = await _finnhubService.GetStockPriceQuote(stockSymbol);

            if (companyProfile != null && stockPriceQuote != null)
            {
                StockTrade stockTrade = new StockTrade()
                {
                    StockSymbol = stockSymbol,
                    StockName = Convert.ToString(companyProfile["name"]),
                    Price = Convert.ToDouble(stockPriceQuote["c"].ToString())
                };

                ViewBag.Token = _configuration["ApiKey"];

                return View(stockTrade);
            }


            return View();
        }
    }
}
