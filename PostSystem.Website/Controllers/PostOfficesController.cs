using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PostSystem.Website.ViewModels;

namespace PostSystem.Website.Controllers
{
    public class PostOfficesController : BaseController<PostOfficeViewModel>
    {
        private async Task<IEnumerable<SelectListItem>> GetCities()
        {
            using(var httpClient = new HttpClient())
            {
                var token = await GetAccessToken();
                if (token == null)Enumerable.Empty<SelectListItem>();
                    RedirectToAction(nameof(HomeController.Error), "Home");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage citiesResponse = await httpClient.GetAsync(WebsiteHelper.citiesUri);
                if (!citiesResponse.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<SelectListItem>();
                }
                string citiesJsonResponse = await citiesResponse.Content.ReadAsStringAsync();
                var cities = JsonConvert.DeserializeObject<IEnumerable<CityViewModel>>(citiesJsonResponse);
                return cities.Select(city => new SelectListItem(city.City_Name, city.Id.ToString()));
            }
        }

        [HttpGet, ActionName("Index")]
        public async Task<ActionResult> GetAll([FromQuery] int city)
        {
            using (var client = new HttpClient())
            {
                var token = await GetAccessToken();
                if (token == null)
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync($"{Uri}?city={city}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<PostOfficeViewModel>>(jsonResponse);

                ViewBag.Cities = await GetCities();
                return View(responseData);
            }
        }

        public override async Task<ActionResult> Create()
        {
            ViewBag.Cities = await GetCities();

            return View();
        }

        public override async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                var token = await GetAccessToken();
                if (token == null)
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync($"{Uri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<PostOfficeViewModel>(jsonResponse);
                var cities = await GetCities();

                ViewBag.Cities = cities;
                
                return View(responseData);
            }
        }

        public PostOfficesController() : base(WebsiteHelper.postOfficesUri) {}
    }
}