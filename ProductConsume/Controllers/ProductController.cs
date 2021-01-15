using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTOCls;
using System.Net.Http;

namespace ProductConsume.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<ProductDTO> products = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:11653/api/");
                var responseTask = client.GetAsync("product");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductDTO>>();
                    readTask.Wait();
                    products = readTask.Result;
                }
                else
                {
                    products = Enumerable.Empty<ProductDTO>();
                    ModelState.
                    AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(products);
        }
        [HttpGet]
        public ActionResult create()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:11653/api/");
                var responseTask = client.GetAsync("category");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<string>>();
                    readTask.Wait();
                    ViewBag.CategoryName = new SelectList(readTask.Result);
                }
                else //web api sent error response 
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View();
        }
        [HttpPost]
        public ActionResult create(ProductDTO product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:11653/api/");
                var postTask = client.PostAsJsonAsync<ProductDTO>("product", product);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(product);
        }
        public ActionResult Edit(int id)
        {
            ProductDTO product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:11653/api/");
                //HTTP GET
                var responseTask = client.GetAsync("product?id=" + id.ToString());
                var responseTask2 = client.GetAsync("category");
                responseTask.Wait();
                var productResult = responseTask.Result;
                responseTask2.Wait();
                var categoryResult = responseTask2.Result;
                if (productResult.IsSuccessStatusCode && categoryResult.IsSuccessStatusCode)
                {
                    var readTask = productResult.Content.ReadAsAsync<ProductDTO>();
                    readTask.Wait();
                    product = readTask.Result;
                    var readTask2 = categoryResult.Content.ReadAsAsync<IList<string>>();
                    readTask2.Wait();
                    ViewBag.CategoryName = new SelectList(readTask2.Result.ToList<string>(), product.CategoryName);
                    //ViewBag.CategoryName = new SelectList(readTask.Result);
                }
                else //web api sent error response 
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(ProductDTO product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:11653/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<ProductDTO>("product", product);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:11653/api/");
                //HTTP DELETE
                var deleteTask = client.DeleteAsync("product/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}