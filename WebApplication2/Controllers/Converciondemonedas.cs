using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text;

namespace WebApplication2.Controllers
{
    public class Converciondemonedas : Microsoft.AspNetCore.Mvc.Controller
    {

        [HttpGet()]
        public async Task<string> Get()
        {

            string responseBody = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://dolarapi.com/v1/dolares/blue");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue
               ("application/json"));

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                response.EnsureSuccessStatusCode();
                responseBody = response.Content.ReadAsStringAsync().Result;
            }

            return responseBody;
        }
    }

}
