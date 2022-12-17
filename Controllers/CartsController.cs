using BikeMvc.Models;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BikeMvc.Controllers
{
    //[NoDirectAccess]
    public class CartsController : Controller
    {
        string Baseurl = "https://localhost:44367/api/";
        //string Baseurl = "https://app-chandruapi.azurewebsites.net/";
        //public async Task<ActionResult<List<Cart>>> myCart(int? id)
        //{

        //    List<Cart> cart = new List<Cart>();
        //    id = (int)HttpContext.Session.GetInt32("Userid");

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(Baseurl);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage Res = await client.GetAsync("Carts/GetCartById?id=" + id);
        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var apiresponse = Res.Content.ReadAsStringAsync().Result;
        //            cart = JsonConvert.DeserializeObject<List<Cart>>(apiresponse);

        //        }
        //    }
        //    if (cart.Count == 0)
        //    {
        //        ViewBag.ErrorMessage = "Cart is Empty";
        //        return View(cart);
        //    }

        //    return View (cart);
        //}
        //public async Task<IActionResult> GetMyCart()
        //{

        //    var id = HttpContext.Session.GetInt32("CustomerID");
        //    List<Cart>? cart = await myCart(id);
        //    return View(cart);
        //}
        public async Task<IActionResult> Index()
        {
            int id = (int)HttpContext.Session.GetInt32("Userid");
            //var sampleContext = _context.BookingTbl.Include(b => b.Movie).Include(b => b.User);
            List<Cart> cr = new List<Cart>();
            List<Cart> cart = new List<Cart>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("Carts");
                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    cr = JsonConvert.DeserializeObject<List<Cart>>(response);
                    cart = (from i in cr
                            where i.Userid == id
                            select i).ToList();
                    if (cart.Count == 0)
                    {
                        ViewBag.ErrorMessage = "Your Cart Is Empty..";
                        return View(cart);
                    }
                }
                return View(cart);
            }
        }
        public IActionResult AddToCart()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Cart cart)
        {
            cart.Productid = (int)HttpContext.Session.GetInt32("Productid");
            cart.Userid = (int)HttpContext.Session.GetInt32("Userid");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                StringContent content = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Carts/AddToCart", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Carts");
                }
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteCart(int id)
        {
            Cart? cart = new Cart();
            //TempData["Id"] = id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                using (var response = await client.GetAsync("Carts/GetCartById?id=" + id))
                {
                    var apiresponse = response.Content.ReadAsStringAsync().Result;
                    cart = JsonConvert.DeserializeObject<Cart>(apiresponse);
                }
            }
            return View(cart);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteCart(Cart c)
        {
            int id = c.Id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                await client.DeleteAsync("Carts/" + id);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditCart(int id)
        {
            Cart? cart = new Cart();
            //TempData["Id"] = id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                using (var response = await client.GetAsync("Carts/GetCartById?id=" + id))
                {
                    var apiresponse = response.Content.ReadAsStringAsync().Result;
                    cart = JsonConvert.DeserializeObject<Cart>(apiresponse);
                }
            }
            return View(cart);
        }

        [HttpPost]
        public async Task<ActionResult> EditCart(Cart cart)
        {
            int id = cart.Id;
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(Baseurl);
                Client.DefaultRequestHeaders.Clear();
                //int id = (int)cart.CartID;
                StringContent content = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");
                var response = await Client.PutAsync("Carts/" + id, content);

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> ProceedToBuy()
        {
            var id = HttpContext.Session.GetInt32("Userid");
            OrderMaster om = new OrderMaster();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync("Carts/ProceedtoBuy?id=" + id,content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    om = JsonConvert.DeserializeObject<OrderMaster>(apiResponse);
                    HttpContext.Session.SetInt32("Masterid", om.OrderMasterid);
                }

            }
            return RedirectToAction("GetPayment", new { id = om.OrderMasterid });
        }
        [HttpGet]
        public async Task<IActionResult> GetPayment(int? id)
        {
            OrderMaster? om = new OrderMaster();
            //TempData["Id"] = id;
            //Cart? cart = new Cart();
            //TempData["Id"] = id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                using (var response = await client.GetAsync("Carts/GetPaymentById?id=" + id))
                {
                    var apiresponse = response.Content.ReadAsStringAsync().Result;
                    om = JsonConvert.DeserializeObject<OrderMaster>(apiresponse);
                }
            }
            return View(om);
            //OrderMaster om = await GetPaymentById((int)id);
            //return View(om);
        }
        [HttpPost]
        public async Task<ActionResult> GetPayment(OrderMaster? m)
        {

            var UserId = HttpContext.Session.GetInt32("Userid");
            m.Userid = (int)UserId;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("Carts/GetPayment", content);
                //return RedirectToAction("GetAllProduct", "Product");
            }
            return RedirectToAction("Thankyou");
        }
        public IActionResult Thankyou()
        {
            return View();
        }

    }
}
