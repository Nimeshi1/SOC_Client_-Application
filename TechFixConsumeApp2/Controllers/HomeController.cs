using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechFixWebApi;

namespace TechFixConsumeApp2.Controllers
{
    public class HomeController : Controller
    {

        // Product view
        public async Task<ActionResult> GetAllProducts()
        {
            List<TechFixWebApi.Product> reservationList = new List<TechFixWebApi.Product>(); using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:62985/api/Products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(reservationList);
        }
        
        // Product Add
        public async Task<ActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> AddProduct(Product du)
        {
            Product dc= new Product();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(du), Encoding.UTF8, "application/json"); using (var response = await httpClient.PostAsync("http://localhost:62985/api/Products", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }
            return View(dc);
        }

        // Update Product
        [HttpGet]
        public async Task<ActionResult> UpdateProduct(int id)
        {
            Product ct = new Product();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync($"http://localhost:62985/api/Products/{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ct = JsonConvert.DeserializeObject<Product>(apiResponse);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error fetching Products details.");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, $"Request error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                }
            }

            return View(ct);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateProduct(Product pr)
        {
            if (!ModelState.IsValid)
            {
                return View(pr);
            }

            using (var httpClient = new HttpClient())
            {
                try
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(pr), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"http://localhost:62985/api/Products/{pr.ProductID}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Optionally, redirect or provide a success message
                            return RedirectToAction("GetAllProducts");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error updating the product.");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    ModelState.AddModelError(string.Empty, $"Request error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                }
            }

            return View(pr);
        }

        // Delete button Product
        [HttpGet]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            Product ro = new Product();
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.DeleteAsync($"http://localhost:62985/api/Products/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                ro = JsonConvert.DeserializeObject<Product>(apiResponse);
                return RedirectToAction("GetAllProducts");
            }
        }

        // Request Quotation view
        public async Task<ActionResult> GetAllRequestQuotation()
        {
            List<TechFixWebApi.RequestQuotation> reservationList = new List<TechFixWebApi.RequestQuotation>(); using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:62985/api/RequestQuotations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<RequestQuotation>>(apiResponse);
                }
            }
            return View(reservationList);
        }
 // Delete button RequestQuotation
        [HttpGet]
        public async Task<ActionResult> DeleteRequestQuotation(int id)
        {
            RequestQuotation rqq = new RequestQuotation();
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.DeleteAsync($"http://localhost:62985/api/RequestQuotations/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                rqq = JsonConvert.DeserializeObject<RequestQuotation>(apiResponse);
                return RedirectToAction("GetAllRequestQuotations");
            }
        }
        // Quotation Add
        public async Task<ActionResult> AddQuotation()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> AddQuotation(Quotation qu)
        {
            Quotation qt = new Quotation();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(qu), Encoding.UTF8, "application/json"); using (var response = await httpClient.PostAsync("http://localhost:62985/api/Quotations", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<Quotation>(apiResponse);
                }
            }
            return View(qt);
        }
    // Order view
        public async Task<ActionResult> GetAllOrder()
        {
            List<TechFixWebApi.Order> reservationList = new List<TechFixWebApi.Order>(); using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:62985/api/Orders"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Order>>(apiResponse);
                }
            }
            return View(reservationList);
        }
        // Delete button Order
        [HttpGet]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            Order orr = new Order();
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.DeleteAsync($"http://localhost:62985/api/Orders/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                orr = JsonConvert.DeserializeObject<Order>(apiResponse);
                return RedirectToAction("GetAllOrders");
            }
        }
    }
}