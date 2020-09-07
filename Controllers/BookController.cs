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
    public class BookController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44325");
        HttpClient client;

        public BookController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;

        }

        public IActionResult GetDetails()
        {
            List<Book> ls = new List<Book>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/Book").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<Book>>(data);
            }
            return View(ls);
        }
        public IActionResult Details(int id)
        {
            Book book = new Book();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/Book/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<Book>(data);
            }

            return View(book);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            string data = JsonConvert.SerializeObject(book);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "api/Book/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetDetails");
            }
            return BadRequest();
        }

        public ActionResult Edit(int id)
        {
            Book book = new Book();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "api/Book/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<Book>(data);
            }
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            string data = JsonConvert.SerializeObject(book);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "api/Book/" + book.Id, content).Result;
            if (response.IsSuccessStatusCode)
                return RedirectToAction("GetDetails");
            return BadRequest();
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "api/Book/" + id).Result;
            if (response.IsSuccessStatusCode)
                return RedirectToAction("GetDetails");
            return BadRequest();
        }
    }
}
