using BikeMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DemoClient.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            Admin admin1 = new Admin();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                StringContent content = new StringContent(JsonConvert.SerializeObject(admin), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44367/api/Credential/AdminLogin", content);
                if (response.IsSuccessStatusCode)
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    admin1 = JsonConvert.DeserializeObject<Admin>(apiresponse);
                    HttpContext.Session.SetString("name", admin1.Username);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ViewBag.ErrorMessageA = "*Invalid Email-ID or Password";
                    //return RedirectToAction("AdminLogin");
                    return View();
                }
            }
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}