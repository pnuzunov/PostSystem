using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostSystem.Website.ViewModels;

namespace PostSystem.Website.Controllers
{
    public abstract class BaseController<TViewModel> : Controller
        where TViewModel : BaseViewModel, new()
    {
        public Uri Uri { get; }

        public BaseController(Uri uri)
        {
            this.Uri = uri;
        }

        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var token = await GetAccessToken();
                client.DefaultRequestHeaders.Add(WebsiteHelper.AUTHORIZATION_HEADER_NAME, token);

                HttpResponseMessage response = await client.GetAsync(Uri);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<TViewModel>>(jsonResponse);

                return View(responseData);
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{Uri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<TViewModel>(jsonResponse);

                return View(responseData);
            }
        }

        public abstract Task<ActionResult> Create();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TViewModel viewModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var serializedContent = JsonConvert.SerializeObject(viewModel);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, WebsiteHelper.JSON_MEDIA_TYPE);

                    HttpResponseMessage response = await client.PostAsync(Uri, stringContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(HomeController.Error), "Home");
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }
        }

        public abstract Task<ActionResult> Edit(int id);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TViewModel viewModel)
        {
            viewModel.Id = id;

            try
            {
                using (var client = new HttpClient())
                {

                    var serializedContent = JsonConvert.SerializeObject(viewModel);
                    var stringContent = new StringContent(serializedContent, Encoding.UTF8, WebsiteHelper.JSON_MEDIA_TYPE);

                    HttpResponseMessage response = await client.PutAsync($"{Uri}/{id}", stringContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(HomeController.Error), "Home");
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{Uri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<TViewModel>(jsonResponse);

                return View(responseData);
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    HttpResponseMessage response = await client.DeleteAsync($"{Uri}/{id}");

                    if (!response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(HomeController.Error), "Home");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }
        }

        private async Task<string> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                var serializedContent = JsonConvert.SerializeObject(new { Username = "test1Username", Password = "test1Password" });
                var stringContent = new StringContent(serializedContent, Encoding.UTF8, WebsiteHelper.JSON_MEDIA_TYPE);

                HttpResponseMessage response = await client.PostAsync(WebsiteHelper.tokenUri, stringContent);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return $"Bearer {await response.Content.ReadAsStringAsync()}";
            }
        }
    }
}