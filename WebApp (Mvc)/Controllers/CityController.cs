using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using CofeeShop.Helper;
using CofeeShop.Models;

namespace CofeeShop.Controllers
{
    public class CityController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public CityController(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_configuration["WebApiBaseUrl"])
            };
        }

        #region Api
        #region GET
        [HttpGet]
        public async Task<IActionResult> CityList()
        {
            HttpResponseMessage response = await _client.GetAsync("api/City");

            if (response.IsSuccessStatusCode)
            {
                JsonOperation<List<CityModel>> jsonOperation = new JsonOperation<List<CityModel>>();

                ApiResultData<List<CityModel>> apiResultData = await jsonOperation.jsonDeserialization(response);
                return View(apiResultData.Data);
            }
            return View();
        }
        #endregion

        #region DELETE
        public async Task<IActionResult> Delete(string CityID)
        {
            int newId = Convert.ToInt32(UrlEncryptor.Decrypt(CityID));
            HttpResponseMessage response = await _client.DeleteAsync($"api/City/{newId}");
            return RedirectToAction("CityList");
        }
        #endregion

        #region AddEdit
        public async Task<IActionResult> CityAddEdit(string? CityID)
        {
            await LoadCountryList();
            if (!string.IsNullOrEmpty(CityID))
            {
                int newId = Convert.ToInt32(UrlEncryptor.Decrypt(CityID));
                var response = await _client.GetAsync($"api/City/{newId}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var city = JsonConvert.DeserializeObject<CityModel>(data);
                    ViewBag.StateList = await GetStatesByCountryID(city.CountryID);
                    return View(city);
                }
            }
            return View(new CityModel());
        }
        #endregion

        #region Save
        [HttpPost]
        public async Task<IActionResult> Save(CityModel city)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(city);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (city.CityID == null)
                    response = await _client.PostAsync("api/City", content);
                else
                    response = await _client.PutAsync($"api/City/{city.CityID}", content);

                if (response.IsSuccessStatusCode)
                {
                    if (city.CityID == null)
                    {
                        ViewBag.CityInsertMessage = "Record Inserted Successfully";
                    }
                    else
                    {
                        ViewBag.CityInsertMessage = "Record Updated Successfully";
                    }
                    return RedirectToAction("CityList");
                }
            }
            await LoadCountryList();
            return View("CityAddEdit", city);
        }

        #endregion

        #region LoadCountryList
        private async Task LoadCountryList()
        {
            var response = await _client.GetAsync("api/City/countries");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<List<CountryDropDownModel>>(data);
                ViewBag.CountryList = countries;
            }
        }
        #endregion

        #region GetStatesByCountry
        [HttpPost]
        public async Task<JsonResult> GetStatesByCountry(int CountryID)
        {
            var states = await GetStatesByCountryID(CountryID);
            return Json(states);
        }

        private async Task<List<StateDropDownModel>> GetStatesByCountryID(int CountryID)
        {
            var response = await _client.GetAsync($"api/City/states/{CountryID}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<StateDropDownModel>>(data);
            }
            return new List<StateDropDownModel>();
        }
        #endregion
        #endregion
    }
}