using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public virtual async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var token = await GetAccessToken();
                if (token == null)
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        public virtual async Task<ActionResult> Details(int id)
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

                var responseData = JsonConvert.DeserializeObject<TViewModel>(jsonResponse);

                return View(responseData);
            }
        }

        public abstract Task<ActionResult> Create();

        [HttpPost]
        public virtual async Task<ActionResult> Create(TViewModel viewModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await GetAccessToken();
                    if (token == null)
                        return RedirectToAction(nameof(HomeController.Error), "Home");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
        public virtual async Task<ActionResult> Edit(int id, TViewModel viewModel)
        {
            viewModel.Id = id;

            try
            {
                using (var client = new HttpClient())
                {
                    var token = await GetAccessToken();
                    if (token == null)
                        return RedirectToAction(nameof(HomeController.Error), "Home");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        public virtual async Task<ActionResult> Delete(int id)
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

                var responseData = JsonConvert.DeserializeObject<TViewModel>(jsonResponse);

                return View(responseData);
            }
        }

        [HttpPost, ActionName("Delete")]
        public virtual async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var token = await GetAccessToken();
                    if (token == null)
                        return RedirectToAction(nameof(HomeController.Error), "Home");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        protected async Task<string> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                string username = HttpContext.Session.GetString("Username");
                string password = HttpContext.Session.GetString("Password");

                if (username == null || password == null)
                    return null;

                var serializedContent = JsonConvert.SerializeObject(new { Username = username, Password = password });
                var stringContent = new StringContent(serializedContent, Encoding.UTF8, WebsiteHelper.JSON_MEDIA_TYPE);
                
                HttpResponseMessage response = await client.PostAsync(WebsiteHelper.tokenUri, stringContent);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}