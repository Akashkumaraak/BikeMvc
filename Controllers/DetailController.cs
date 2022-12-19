using BikeMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BikeMvc.Controllers
{
    public class DetailController : Controller
    {
        public async Task<IActionResult> Index()
        {
           
            List<OrderDetail> details = new List<OrderDetail>();
           
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("https://localhost:44367/api/Detail");
                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    details = JsonConvert.DeserializeObject<List<OrderDetail>>(response);
                }
                return View(details);
            }
        }
    }
}
