using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.Catalog.Products;
using VegetableShop.Data.EF;
using VegetableShop.Data.Entitis;


namespace VegetableShop.WebApp.Controllers
{
	public class ProductsController : Controller
	{
        private readonly VegetableShopDbContext _context;
        private readonly IProductManager _productManager;

        public ProductsController(VegetableShopDbContext context,IProductManager productManager) 
		{
			_context = context;
			_productManager = productManager;
		}
		public async Task<IActionResult> Index()
		{
			var products = _context.Products.Include(c=>c.Category);
			return View(await products.ToListAsync());
		}
		public async Task<IActionResult> Detail(int Id)
		{
			if (Id == 0 || _context.Products == null) return NotFound();
            var product = await _context.Products.FindAsync(Id);
			if (product == null) return NotFound();
			return View(product);
		}
        [HttpGet]
		public IActionResult Create()
		{
			ViewData["CatecoryId"] = new SelectList(_context.Categories,"ID","Name");
			return View();
		}
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateUpdate request)
		{
			if(ModelState.IsValid)
			{
                var category =await  _context.Categories.FindAsync(request.CategoryId);
                if (category == null) 
                {
                    ViewBag.error = $"Create error,{category}";
                    return View(); 
                }
				var product = await  _productManager.CreateProdcut(request);
                if (product == 0)
                {
					ViewBag.error = $"Create error,{product}";
					return View();
				}
				return RedirectToAction(nameof(Index));
			}
            ViewData["CatecoryId"] = new SelectList(_context.Categories, "ID", "Name",request.CategoryId);
			return View();
        }
		[HttpGet]
		public async Task<IActionResult> Update(int Id)
		{
			if(Id==0|| _context.Products == null) { return NotFound(); }
			var product =await  _context.Products.FindAsync(Id);
			if (product == null) return NotFound();
            ViewData["CatecoryId"] = new SelectList(_context.Categories, "ID", "Name",product.CategoryId);
			return View(product);
        }
		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult>Update(int Id,ProductCreateUpdate request)
		{
			if (ModelState.IsValid)
            {
                try
				{
                    var product = await  _productManager.UpdateProdcut(Id, request);
                    if (product == 0)
                    {
						ViewBag.error = $"Update error,{product}";
						return View();
					}
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(Id))
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
            ViewData["CatecoryId"] = new SelectList(_context.Categories, "ID", "Name",request.CategoryId);
            return View();
        }

		[HttpGet]
		public async Task<IActionResult> Delete(int? Id)
		{
            if (Id == 0 || _context.Products == null) { return NotFound(); }
            var product = await _context.Products.FindAsync(Id);
            if (product == null) return NotFound();
            ViewData["CatecoryId"] = new SelectList(_context.Categories, "ID", "Name",product.CategoryId);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var product =await  _productManager.DeleteProduct(Id);
            if (!product)
            {
				ViewBag.error = $"Delete error,{product}";
				return View();
			}
            return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		public IActionResult Search(SearchProdcut request)
        {
			var prodcut = _productManager.Search(request);
			return View(prodcut);
		}
            private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
