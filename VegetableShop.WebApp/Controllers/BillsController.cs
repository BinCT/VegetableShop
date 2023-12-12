using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VegetableShop.Data.Catalog.Bill;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.EF;

namespace VegetableShop.WebApp.Controllers
{
    public class BillsController : Controller
    {
        private readonly VegetableShopDbContext _context;
        private readonly IBillManager _bill;

        public BillsController(VegetableShopDbContext context,IBillManager billManager)
        {
            _context = context;
            _bill = billManager;
        }
        public async Task<IActionResult> Index()
        {
            var bill = _context.Bills;
            return View(await bill.ToListAsync());
        }
        public async Task<IActionResult> Details(int Id)
        {
            if (Id == 0 || _context.Bills == null) return NotFound();
            var bill =await  _context.Bills.FindAsync(Id);
            if (bill == null) return NotFound();
            return View(bill);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Customer"] = new SelectList(_context.Customers,"Id","Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BillCreateUpdate request)
        {
            if (ModelState.IsValid)
            {
                var bill = await _bill.CreateBill(request);
                if (bill == 0)
                {
                    ViewBag.error = $"Create error {bill}";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Customer"] = new SelectList(_context.Customers, "Id", "Name");
            return View();
        }
        [HttpGet]
        public IActionResult Update(int Id)
        {
            if (Id == 0 || _context.Bills == null) return NotFound();
            ViewData["Customer"] = new SelectList(_context.Customers, "Id", "Name",Id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int Id,BillCreateUpdate request)
        {
            if (ModelState.IsValid)
            {
                var bill = await _bill.UpdateBill(Id, request);
                if (bill == 0)
                {
                    ViewBag.error = $"Update error {bill}";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == 0 || _context.Bills == null) return NotFound();
            ViewData["Customer"] = new SelectList(_context.Customers, "Id", "Name", Id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Delete(int Id)
        {
            if (Id == 0) return NotFound();
            var bill =await  _bill.DeleteBill(Id);
            if (!bill)
            {
                ViewBag.error = $"Delete error {bill}";
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
