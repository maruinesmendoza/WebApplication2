using Microsoft.AspNetCore.Mvc;

using Nancy.Json;

using Newtonsoft.Json;

using System.Net.Http.Headers;
using System.Text;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly HttpClient httpClient;
        public CurrencyController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        [HttpPost]
        public async Task<QuoteCurrencyResponse> Post(RequestCurrency requestCurrency)
        {
            return await PostDolar(requestCurrency);
        }

        private async Task<QuoteCurrencyResponse> PostDolar(RequestCurrency requestCurrency)
        {
            var jsonObject = new JavaScriptSerializer().Serialize(requestCurrency);

            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8,"application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync("https://localhost:7238/api/Quote", content);

            if (!response.IsSuccessStatusCode)
            {
                var httpResponse = new HttpResponseMessage(response.StatusCode);

                httpResponse.ReasonPhrase = " CONNECTION ERROR";
                httpResponse.Content = new StringContent("");
                throw new HttpRequestException();
            }

            string str = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<QuoteCurrencyResponse>(str);
        }
    }

}