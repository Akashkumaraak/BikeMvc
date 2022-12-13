using BikeMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DemoClient.Controllers
{
    public class UserController : Controller
    {
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44341/api/Credentials", content);
                ViewBag.SuccessMessageA = "You are successfully registered...";
                return RedirectToAction("Login", "User");
            }
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            User user1 = new User();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44367/api/Credential/UserLogin/", content);
                if (response.IsSuccessStatusCode)
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    user1 = JsonConvert.DeserializeObject<User>(apiresponse);
                    //client.DefaultRequestHeaders.Clear();
                    //StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    //var response = await client.PostAsync("https://localhost:44367/api/Credential/UserLogin", content);
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    string apiresponse = await response.Content.ReadAsStringAsync();
                    //    user1 = JsonConvert.DeserializeObject<User>(apiresponse);
                    HttpContext.Session.SetString("Username", user1.UserName);
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