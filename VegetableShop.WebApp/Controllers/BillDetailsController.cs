using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VegetableShop.Data.Catalog.BillDetail;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.EF;
using VegetableShop.Data.Entitis;

namespace VegetableShop.WebApp.Controllers
{
    public class BillDetailsController : Controller
    {
        private readonly VegetableShopDbContext _context;
        private readonly IBillDetailManager _billdetail;

        public BillDetailsController(VegetableShopDbContext context,IBillDetailManager billDetail) 
        {
            _context = context;
            _billdetail=billDetail;
        }

        // GET: BillDetailsController
        public IActionResult Index()
        {
            var billdetail = _context.BillDetails.Include(c=>c.Bill).Include(c => c.Product);
            return View(billdetail);
        }

        // GET: BillDetailsController/Details/5
        public async Task<IActionResult> Details(int billid, int productid)
        {
            if (billid==0||productid == 0 || _context.BillDetails == null) return NotFound();
            var billdetail =await _context.BillDetails.FindAsync(billid, productid);
            if(billdetail == null) return NotFound();
            ViewData["Prodcut"] = new SelectList(_context.Products, "Id", "Name");
            return View(billdetail);
        }

        // GET: BillDetailsController/Create
        public ActionResult Create()
        {
            ViewData["Bill"] = new SelectList(_context.Bills, "ID", "ID");
            ViewData["Prodcut"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: BillDetailsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BillDetailCreateUpdate request)
        {
            if (ModelState.IsValid)
            {

                    var billdetail = await _billdetail.Create(request);
                    if (billdetail == 0)
                        {
                            ViewBag.error = $"Create error {billdetail}";
                            return View();
                        }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bill"] = new SelectList(_context.Bills, "ID", "ID");
            ViewData["Prodcut"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // GET: BillDetailsController/Edit/5
        public ActionResult Update(int billid, int productid)
        {
            if(billid == 0 || productid == 0 ||_context.BillDetails==null) return NotFound();
            ViewData["Prodcut"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: BillDetailsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int billid, int productid, BillDetailCreateUpdate request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var billdetail = await _billdetail.Update(billid,productid,request);
                    if (billdetail == 0)
                    {
                        ViewBag.error = $"update error {billdetail}";
                        return View();
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ViewData["Bill"] = new SelectList(_context.Bills, "ID", "ID");
            ViewData["Prodcut"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // GET: BillDetailsController/Delete/5
        public ActionResult Delete(int? billid, int? productid)
        {
            if (billid == 0 || productid == 0 || _context.BillDetails == null) return NotFound();
            ViewData["Prodcut"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: BillDetailsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int billid, int productid)
        {
            if (billid == 0 || productid == 0) return NotFound();
            var billdetail = await _billdetail.Delete(billid, productid);
            if(!billdetail)
            {
                ViewBag.error = $"Create error {billdetail}";
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
