﻿using System;
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
    public class DeliveriesController : BaseController<DeliveryViewModel>
    {
        private async Task<IEnumerable<SelectListItem>> GetMails()
        {
            using (var httpClient = new HttpClient())
            {
                var token = await GetAccessToken();
                if (token == null)
                    return Enumerable.Empty<SelectListItem>();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage mailsResponse = await httpClient.GetAsync(WebsiteHelper.mailsUri);
                if (!mailsResponse.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<SelectListItem>();
                }
                string mailsJsonResponse = await mailsResponse.Content.ReadAsStringAsync();
                var mails = JsonConvert.DeserializeObject<IEnumerable<MailViewModel>>(mailsJsonResponse);
                return mails.Select(mail => new SelectListItem(mail.Description, mail.Id.ToString()));
            }
        }

        private async Task<IEnumerable<SelectListItem>> GetOffices()
        {
            using (var httpClient = new HttpClient())
            {
                var token = await GetAccessToken();
                if (token == null)
                    return Enumerable.Empty<SelectListItem>();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage officesResponse = await httpClient.GetAsync(WebsiteHelper.postOfficesUri);
                if (!officesResponse.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<SelectListItem>();
                }
                string officesJsonResponse = await officesResponse.Content.ReadAsStringAsync();
                var offices = JsonConvert.DeserializeObject<IEnumerable<PostOfficeViewModel>>(officesJsonResponse);
                return offices.Select(office => new SelectListItem($"{office.Office_City.City_Name}, {office.Address}", office.Id.ToString()));
            }
        }

        [HttpGet, ActionName("Index")]
        public async Task<ActionResult> GetAll([FromQuery] string details)
        {
            using (var client = new HttpClient())
            {
                var token = await GetAccessToken();
                if (token == null)
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync($"{Uri}?details={details}");

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<DeliveryViewModel>>(jsonResponse);

                return View(responseData);
            }
        }


        public override async Task<ActionResult> Create()
        {
            ViewBag.Mails = await GetMails();
            ViewBag.PostOffices = await GetOffices();

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

                var responseData = JsonConvert.DeserializeObject<DeliveryViewModel>(jsonResponse);

                ViewBag.Mails = await GetMails();
                ViewBag.PostOffices = await GetOffices();

                return View(responseData);
            }
        }

        public DeliveriesController() : base(WebsiteHelper.deliveriesUri) { }
    }
}