using CofeeShop.Helper;
using CofeeShop.Models;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CofeeShop.Controllers
{
    public class CountryController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public CountryController(IConfiguration configuration)
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
        public async Task<IActionResult> CountryList()
        {
            HttpResponseMessage response = await _client.GetAsync("api/Country");

            if (response.IsSuccessStatusCode)
            {
                JsonOperation<List<CountryModel>> jsonOperation = new JsonOperation<List<CountryModel>>();

                ApiResultData<List<CountryModel>> apiResultData = await jsonOperation.jsonDeserialization(response);
                return View(apiResultData.Data);
            }
            return View();
        }
        #endregion

        #region DELETE
        public async Task<IActionResult> Delete(string CountryID)
        {
            int newId = Convert.ToInt32(UrlEncryptor.Decrypt(CountryID));
            HttpResponseMessage response = await _client.DeleteAsync($"api/Country/{newId}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["DeleteErrorMessage"] = "ForeignKey Conflict!";
            }
            return RedirectToAction("CountryList");
        }
        #endregion

        #region AddEdit
        public async Task<IActionResult> CountryAddEdit(string? CountryID)
        {
            if (!string.IsNullOrEmpty(CountryID))
            {
                int newId = Convert.ToInt32(UrlEncryptor.Decrypt(CountryID));
                var response = await _client.GetAsync($"api/Country/{CountryID}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var country = JsonConvert.DeserializeObject<CountryModel>(data);
                    return View(country);
                }
            }
            return View(new CountryModel());
        }
        #endregion

        #region Save
        [HttpPost]
        public async Task<IActionResult> Save(CountryModel country)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(country);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (country.CountryID == null)
                    response = await _client.PostAsync("api/Country", content);
                else
                    response = await _client.PutAsync($"api/Country/{country.CountryID}", content);

                if (response.IsSuccessStatusCode)
                {
                    if (country.CountryID == null)
                    {
                        TempData["CountryInsertMessage"] = "Record Inserted Successfully";
                    }
                    else
                    {
                        TempData["CountryInsertMessage"] = "Record Updated Successfully";
                    }
                    return RedirectToAction("CountryList");
                }
            }
            return View("CountryAddEdit", country);
        }
        #endregion

        #endregion

    }
}
