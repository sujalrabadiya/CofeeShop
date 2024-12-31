using CofeeShop.Helper;
using CofeeShop.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CofeeShop.Controllers
{
    public class StateController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public StateController(IConfiguration configuration)
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
        public IActionResult StateList()
        {
            List<StateModel> states = new List<StateModel>();
            HttpResponseMessage response = _client.GetAsync("api/State").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                states = JsonConvert.DeserializeObject<List<StateModel>>(data);
            }
            return View(states);
        }
        #endregion

        #region DELETE
        public async Task<IActionResult> Delete(string StateID)
        {
            int newId = Convert.ToInt32(UrlEncryptor.Decrypt(StateID));
            HttpResponseMessage response = await _client.DeleteAsync($"api/State/{newId}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["DeleteErrorMessage"] = "ForeignKey Conflict!";
            }
            return RedirectToAction("StateList");
        }
        #endregion

        #region AddEdit
        public async Task<IActionResult> StateAddEdit(string? StateID)
        {
            await LoadCountryList();
            if (!string.IsNullOrEmpty(StateID))
            {
                int? newId = Convert.ToInt32(UrlEncryptor.Decrypt(StateID));
                var response = await _client.GetAsync($"api/State/{StateID}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var state = JsonConvert.DeserializeObject<StateModel>(data);
                    return View(state);
                }
            }
            return View(new StateModel());
        }
        #endregion

        #region Save
        [HttpPost]
        public async Task<IActionResult> Save(StateModel state)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(state);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                if (state.StateID == null)
                    response = await _client.PostAsync("api/State", content);
                else
                    response = await _client.PutAsync($"api/State/{state.StateID}", content);

                if (response.IsSuccessStatusCode)
                {
                    if (state.StateID == null)
                    {
                        TempData["StateInsertMessage"] = "Record Inserted Successfully";
                    }
                    else
                    {
                        TempData["StateInsertMessage"] = "Record Updated Successfully";
                    }
                    return RedirectToAction("StateList");
                }
            }
            await LoadCountryList();
            return View("StateAddEdit", state);
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

        #endregion
    }
}
