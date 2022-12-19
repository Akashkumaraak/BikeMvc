using BikeMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BikeMvc.Controllers
{
    public class MasterController : Controller
    {
        public async Task<IActionResult> Index()
        {

            List<OrderMaster> master = new List<OrderMaster>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44367/api/Master");
                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    master = JsonConvert.DeserializeObject<List<OrderMaster>>(response);
                }
                return View(master);
            }
        }
    }
}