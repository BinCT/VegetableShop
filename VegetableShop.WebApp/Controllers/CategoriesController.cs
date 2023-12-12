using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.EF;
using VegetableShop.WebApp.Models.Categories;

namespace VegetableShop.WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly VegetableShopDbContext _context;
        private readonly ICategoryManager _categoryManager;
        public CategoriesController(VegetableShopDbContext context, ICategoryManager categoryManager)
        {
            _context = context;
            _categoryManager = categoryManager;
        }
        public async Task<IActionResult> Index()
        {
            var category = _context.Categories;
            if (category == null) return NotFound();
            return View(await category.ToListAsync());
        }
        public async Task<IActionResult> Details(int Id)
        {
            if (Id == 0 || _context.Categories == null) return NotFound();
            var category = await _context.Categories.FindAsync(Id);
            if (category == null) return NotFound();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateUpdate request)
        {
            if(ModelState.IsValid)
            {
                var category = await _categoryManager.Create(request);
                if (category == 0)
                {
                    ViewBag.error = $"Create Error {category}";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0 || _context.Categories == null) return NotFound();
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int Id,CategoryCreateUpdate request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = await _categoryManager.Update(Id, request);
                    if (category == 0)
                    {
                        ViewBag.error = $"update Error {category}";
                        return View();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || _context.Categories == null) return NotFound();
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var category = await _categoryManager.Delete(Id);
            if (!category)
            {
                ViewBag.error = $"Delete Error {category}";
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        private bool CategoryExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
