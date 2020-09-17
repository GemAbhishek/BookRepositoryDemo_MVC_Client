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
    public class SellController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44325");
        HttpClient client;

        public SellController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;

        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SellRecord book)
        {
            //string data = JsonConvert.SerializeObject(book);
            //StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "api/Sell/" + book.Id + "/" + book.SellQty, null).Result;
            if (response.IsSuccessStatusCode)
                return RedirectToAction("GetDetails", "SellRecord");
            return RedirectToAction("Error");
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}
