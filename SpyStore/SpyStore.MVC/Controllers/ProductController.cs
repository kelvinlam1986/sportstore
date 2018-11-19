using Microsoft.AspNetCore.Mvc;
using SpyStore.Models.ViewModels.Base;
using SpyStore.MVC.WebServiceAccess.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpyStore.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;

        public ProductController(IWebApiCalls webApiCalls)
        {
            _webApiCalls = webApiCalls;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Featured));
        }

        public ActionResult Details(int id)
        {
            return RedirectToAction(
                nameof(CartController.AddToCart),
                nameof(CartController).Replace("Controller", ""),
                new { customerId = ViewBag.CustomerId, productId = id, cameFromProduct = true });
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Featured()
        {
            ViewBag.Title = "Featured Products";
            ViewBag.Header = "Featured Products";
            ViewBag.ShowCategory = true;
            ViewBag.Featured = true;
            return await GetListOfProducts(featured: true);
        }

        [HttpGet]
        public async Task<IActionResult> ProductList(int id)
        {
            var cat = await _webApiCalls.GetCategoryAsync(id);
            ViewBag.Title = cat?.CategoryName;
            ViewBag.Header = cat?.CategoryName;
            ViewBag.ShowCategory = false;
            ViewBag.Featured = false;
            return await GetListOfProducts(featured: false);
        }

        [Route("[controller]/[action]")]
        [HttpPost("{searchString}")]
        public async Task<IActionResult> Search(string searchString)
        {
            ViewBag.Title = "Search Results";
            ViewBag.Header = "Search Results";
            ViewBag.ShowCategory = true;
            ViewBag.Featured = false;
            return await GetListOfProducts(searchString: searchString);
        }

        internal async Task<IActionResult> GetListOfProducts(
            int id = -1, bool featured = false, string searchString = "")
        {
            IList<ProductAndCategoryBase> products;
            if (featured)
            {
                products = await _webApiCalls.GetFeaturedProductsAsync();
            }
            else if (!string.IsNullOrEmpty(searchString))
            {
                products = await _webApiCalls.SearchAsync(searchString);
            }
            else
            {
                products = await _webApiCalls.GetProductsForACategoryAsync(id);
            }

            if (products == null)
            {
                return NotFound();
            }

            return View("ProductList", products);
        }
    }
}