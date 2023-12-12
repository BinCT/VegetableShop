using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using VegetableShop.Data.Catalog.Customers;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.EF;

namespace VegetableShop.WebApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly VegetableShopDbContext _context;
        private readonly ICustomerManager _customer;

        public CustomersController(VegetableShopDbContext context,ICustomerManager customer) 
        {
            _context = context;
            _customer = customer;
        }
        public IActionResult Index()
        {
            var customer = _context.Customers;
            return View(customer);
        }
        public async Task<IActionResult> Details(string Id)
        {
            if (Id == null || _context.Customers == null) return NotFound();
            var customer =await  _context.Customers.FindAsync(Id);
            if(customer == null) return NotFound();
            return View(customer);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreate request)
        {

			if (ModelState.IsValid)
            {
                var customer =await  _customer.Create(request);
                if (customer != 0)
                {
					return RedirectToAction(nameof(Index));
					
                }
            }
			ViewBag.error = "Create Error";
			return View();
		}
        public async Task<IActionResult> Update(string Id)
        {
            if (Id == null || _context.Customers == null) return NotFound();
            var customer = await _context.Customers.FindAsync(Id);
            if (customer == null) return NotFound();
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string Id,CustomerUpdate request)
        {
			if (ModelState.IsValid)
            {
                var customer = await _customer.UpdateCustomer(Id, request);
                if (customer != 0) return RedirectToAction(nameof(Index));
            }
			ViewBag.error = "Update Error";
			return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string? Id)
        {
            if (Id == null || _context.Customers == null) return NotFound();
            var customer = await _context.Customers.FindAsync(Id);
            if (customer == null) return NotFound();
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid Id)
        {
			var customer = await _customer.DeleteById(Id.ToString());
            if (customer) return RedirectToAction(nameof(customer));
			ViewBag.error = "Delete Error";
			return View();
		}
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(CustomerLogin request)
        {
			if (ModelState.IsValid)
            {
                var customer = await _customer.Login(request);
                if (customer)
                {
					ViewBag.error = " Done";
					return View();
                }
            }
			ViewBag.error = "Login Error";
			return View();
        }
        public IActionResult ForPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForPassword(string username,string email,CustomerForgotPassword request)
        {
			if (ModelState.IsValid)
            {

				var customer = await _customer.UpdateForgotPassword(username, email,request);
                if (customer != 0)
                {
					ViewBag.error = "Thanh cong";
					return View();
                }
            }
			ViewBag.error = "Loi";
			return View();
        }
    }
}
