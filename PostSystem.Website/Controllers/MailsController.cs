using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostSystem.Website.ViewModels;

namespace PostSystem.Website.Controllers
{
    public class MailsController : BaseController<MailViewModel>
    {

        public override async Task<ActionResult> Create()
        {
            return View();
        }

        public override async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                var token = await GetAccessToken();
                client.DefaultRequestHeaders.Add(WebsiteHelper.AUTHORIZATION_HEADER_NAME, token);

                HttpResponseMessage response = await client.GetAsync($"{Uri}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<MailViewModel>(jsonResponse);

                return View(responseData);
            }
        }

        public MailsController() : base(WebsiteHelper.mailsUri) {}
    }
}