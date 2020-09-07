using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookRepositoryDemoMVC_Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookRepositoryDemoMVC_Client.Controllers
{
    public class SellRecordController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44325");
        HttpClient client;

        public SellRecordController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;

        }

        public IActionResult GetDetails()
        {
            List<SellRecord> ls = new List<SellRecord>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/SellRecord").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SellRecord>>(data);
            }
            return View(ls);
        }
        public IActionResult Details(int id)
        {
            SellRecord book = new SellRecord();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/SellRecord/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<SellRecord>(data);
            }

            return View(book);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SellRecord book)
        {
            string data = JsonConvert.SerializeObject(book);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/SellRecord/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetDetails");
            }
            return BadRequest();
        }

        public ActionResult Edit(int id)
        {
            SellRecord book = new SellRecord();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/SellRecord/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<SellRecord>(data);
            }
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(SellRecord book)
        {
            string data = JsonConvert.SerializeObject(book);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "api/SellRecord/" + book.Id, content).Result;
            if (response.IsSuccessStatusCode)
                return RedirectToAction("GetDetails");
            return BadRequest();
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "api/SellRecord/" + id).Result;
            if (response.IsSuccessStatusCode)
                return RedirectToAction("GetDetails");
            return BadRequest();
        }
    }
}
