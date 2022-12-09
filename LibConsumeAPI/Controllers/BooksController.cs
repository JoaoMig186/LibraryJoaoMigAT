using Lib.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AppConsumeWebApi.Controllers
{
    public class BooksController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Book> booksList = new List<Book>();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Books"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    booksList = JsonConvert.DeserializeObject<List<Book>>(apiResponse);

                }
            }
            return View(booksList);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            Book addBook = new Book();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:5001/api/Books", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    addBook = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return View(addBook);
        }

        public async Task<IActionResult> Update(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            Book book = new Book();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Books/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Book book)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            Book receivedBook = new Book();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                StringContent content = new StringContent(JsonConvert
                    .SerializeObject(book), Encoding.UTF8, "application/json");

                Console.WriteLine(content.ToString());

                using (var response = await httpClient.PutAsync("https://localhost:5001/api/Books/" + book.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedBook = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return View(receivedBook);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.DeleteAsync("https://localhost:5001/api/Books/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }

            }
            return RedirectToAction("Index");

        }
    }
}