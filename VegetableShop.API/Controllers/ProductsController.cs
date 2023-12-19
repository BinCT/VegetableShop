using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.Catalog.Products;
using VegetableShop.Data.EF;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VegetableShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly VegetableShopDbContext _context;
        private readonly IProductManager _productManager;

        public ProductsController(VegetableShopDbContext context,IProductManager productManager)
        {
            _context = context;
            _productManager = productManager;
        }
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var product = _context.Products.Select(x=>  new ProductGetById()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Quantity = x.Quantity,
                Price = x.Price,
                CategoryId = x.CategoryId,
                FilePast = x.FilePast,
            });
            return Ok(product);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdProduct(int Id)
        {
            var products =await  _productManager.GetById(Id);
            if(products == null) return NotFound();
            return Ok(products);
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm]ProductCreateUpdate request)
        {
            var productId = await _productManager.CreateProdcut(request);
            if (productId == 0) return BadRequest("Create error");
            var product = await _productManager.GetById(productId);
            return CreatedAtAction(nameof(GetByIdProduct),new {Id =  productId},product);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateProduct(int Id,[FromForm]ProductCreateUpdate request)
        {
            var product = await _productManager.UpdateProdcut(Id, request);
            if (product == 0) return BadRequest("Update error");
            return Ok();
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var product = await _productManager.DeleteProduct(Id);
            if (!product) return BadRequest("Delete error");
            return Ok();
        }

        [HttpGet("Page")]
        public IActionResult Search([FromForm] SearchProdcut search)
        {

            try
            {
                var product =_productManager.Search(search);
                return Ok(product);
            }
            catch
            {
                return BadRequest("search error");
            }
        }
    }
}
