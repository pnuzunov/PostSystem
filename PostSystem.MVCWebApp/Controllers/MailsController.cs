using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostSystem.MVCWebApp.ViewModels;

namespace PostSystem.MVCWebApp.Controllers
{
    public class MailsController : Controller
    {
        private readonly Uri mailsUri = new Uri("http://localhost:54320/api/mails");

        // GET: Mails
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient() )
            {
                HttpResponseMessage response = await client.GetAsync(mailsUri);

                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(HomeController.Error), "Home");
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<IEnumerable<MailViewModel>>(jsonResponse);

                return View(responseData);
            }
        }

        // GET: Mails/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Mails/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Mails/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mails/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}