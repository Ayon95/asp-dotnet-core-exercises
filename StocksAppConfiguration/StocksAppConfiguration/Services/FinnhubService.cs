using StocksAppConfiguration.ServiceContracts;
using System.Text.Json;

namespace StocksAppConfiguration.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>?> SendRequest(string url)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                var token = _configuration["ApiKey"];
                var requestUrl = $"{url}&token={token}";

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

                Stream responseStream = responseMessage.Content.ReadAsStream();
                StreamReader streamReader = new StreamReader(responseStream);

                string response = streamReader.ReadToEnd();

                Dictionary<string, object>? responseData = JsonSerializer.Deserialize<Dictionary<string, object>?>(response);

                return responseData;

            }
        }
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            string urlWithoutToken = $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}";
            return await SendRequest(urlWithoutToken);
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            string urlWithoutToken = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}";
            return await SendRequest(urlWithoutToken);
        }
    }
}
