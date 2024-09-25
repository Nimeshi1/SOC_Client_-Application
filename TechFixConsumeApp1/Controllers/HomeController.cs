using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using TechFixWebApi;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;


namespace TechFixConsumeApp1.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> GetAllSuppliers()
        {
            List<TechFixWebApi.Supplier> reservationList = new List<TechFixWebApi.Supplier>(); using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:62985/api/Suppliers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Supplier>>(apiResponse);
                }
            }
            return View(reservationList);
        }

// Add supplier create new
        public async Task<ActionResult> AddSupplier()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> AddSupplier(Supplier pr)
        {
            Supplier s = new Supplier();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(pr), Encoding.UTF8, "application/json"); using (var response = await httpClient.PostAsync("http://localhost:62985/api/Suppliers", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<Supplier>(apiResponse);
                }
            }
            return View(s);
        }
   // Update supplier
        [HttpGet]
        public async Task<ActionResult> UpdateSupplier(int id)
        {
            Supplier ss = new Supplier();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync($"http://localhost:62985/api/Suppliers/{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ss = JsonConvert.DeserializeObject<Supplier>(apiResponse);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error fetching supplier details.");
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

            return View(ss);
        }
[HttpPost]
        public async Task<ActionResult> UpdateSupplier(Supplier pr)
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
                    using (var response = await httpClient.PutAsync($"http://localhost:62985/api/Suppliers/{pr.SupplierID}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Optionally, redirect or provide a success message
                            return RedirectToAction("GetAllSuppliers");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error updating the supplier.");
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
        // Delete button Supplier
        [HttpGet]
        public async Task<ActionResult> DeleteSupplier(int id)
        {
            Supplier p = new Supplier();
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.DeleteAsync($"http://localhost:62985/api/Suppliers/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                p = JsonConvert.DeserializeObject<Supplier>(apiResponse);
                return RedirectToAction("GetAllSuppliers");
            }
        }
//Product view
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

        //Request quotation Add
        // create new
        public async Task<ActionResult> AddRequestQuotation()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> AddRequestQuotation(RequestQuotation rq)
        {
            RequestQuotation r = new RequestQuotation();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(rq), Encoding.UTF8, "application/json"); using (var response = await httpClient.PostAsync("http://localhost:62985/api/RequestQuotations", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<RequestQuotation>(apiResponse);
                }
            }
            return View(r);
        }
        //Quotation view
        public async Task<ActionResult> GetAllQuotation()
        {
            List<TechFixWebApi.Quotation> reservationList = new List<TechFixWebApi.Quotation>(); using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:62985/api/Quotations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Quotation>>(apiResponse);
                }
            }
            return View(reservationList);
        }
        // Delete button Quotation
        [HttpGet]
        public async Task<ActionResult> DeleteQuotation(int id)
        {
            Quotation q = new Quotation();
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.DeleteAsync($"http://localhost:62985/api/Quotations/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                q = JsonConvert.DeserializeObject<Quotation>(apiResponse);
                return RedirectToAction("GetAllQuotation");
            }
        }
//Order view
        public async Task<ActionResult> GetAllOrders()
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

        //Order Add
        public async Task<ActionResult> AddOrder()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> AddOrder(Order or)
        {
            Order rd = new Order();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(or), Encoding.UTF8, "application/json"); using (var response = await httpClient.PostAsync("http://localhost:62985/api/Orders", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<Order>(apiResponse);
                }
            }
            return View(rd);
        }

        // Update order
        [HttpGet]
        public async Task<ActionResult> UpdateOrder(int id)
        {
            Order oo = new Order();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync($"http://localhost:62985/api/Orders/{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            oo = JsonConvert.DeserializeObject<Order>(apiResponse);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error fetching order details.");
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

            return View(oo);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateOrder(Order or)
        {
            if (!ModelState.IsValid)
            {
                return View(or);
            }

            using (var httpClient = new HttpClient())
            {
                try
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(or), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"http://localhost:62985/api/Orders/{or.OrderID}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Optionally, redirect or provide a success message
                            return RedirectToAction("GetAllOrders");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error updating the order.");
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

            return View(or);
        }
        // Delete button Order
        [HttpGet]
        public async Task<ActionResult> DeleteOrders(int id)
        {
            Order e = new Order();
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.DeleteAsync($"http://localhost:62985/api/Orders/{id}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                e = JsonConvert.DeserializeObject<Order>(apiResponse);
                return RedirectToAction("GetAllOrders");
            }
        }


    }

}
