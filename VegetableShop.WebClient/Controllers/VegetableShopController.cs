using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.Common;
using VegetableShop.Data.EF;

namespace VegetableShop.WebClient.Controllers
{
    public class VegetableShopController : Controller
    {
        private readonly VegetableShopDbContext _context;
        private readonly IProductManager _productManager;
        public VegetableShopController(VegetableShopDbContext context, IProductManager productManager)
        {
            _context = context;
            _productManager = productManager;
        }

        public  IActionResult Index()
        {
            var product =_context.Products.Include(x=>x.Category);
            
            return View(product);
        }
        public IActionResult Shop()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult ShopDetails()
        {
            return View();
        }
        public IActionResult ShopingCart()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        public IActionResult BlogDetails()
        {
            return View();
        }
    }
}
