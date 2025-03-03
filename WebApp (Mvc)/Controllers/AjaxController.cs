using CofeeShop.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CofeeShop.Controllers
{
    public class AjaxController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public AjaxController(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_configuration["WebApiBaseUrl"])
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetCountries()
        {
            var response = await _client.GetAsync("api/country");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<List<CountryModel>>(data);
                return Json(countries);
            }
            return Json(new List<CountryModel>());
        }

        [HttpPost]
        public async Task<JsonResult> SaveCountry([FromBody] CountryModel country)
        {
            HttpResponseMessage response;

            if (country.CountryID > 0)
            {
                var jsonContent = JsonConvert.SerializeObject(country);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                response = await _client.PutAsync($"api/country/{country.CountryID}", content);
            }
            else
            {
                var jsonContent = JsonConvert.SerializeObject(country);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                response = await _client.PostAsync($"api/country", content);
            }

            return Json(response.IsSuccessStatusCode);
        }


        [HttpDelete]
        public async Task<JsonResult> DeleteCountry(int id)
        {
            var response = await _client.DeleteAsync($"api/country/{id}");
            return Json(response.IsSuccessStatusCode);
        }
    }
}
