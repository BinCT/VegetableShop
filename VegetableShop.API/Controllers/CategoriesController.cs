using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using VegetableShop.Data.Catalog.Categories;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.EF;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VegetableShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly VegetableShopDbContext _context;
        private readonly ICategoryManager _catogory;

        public CategoriesController(VegetableShopDbContext context,ICategoryManager categoryManager)
        {
            _context = context;
            _catogory = categoryManager;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var category = _context.Categories.Select(x=> new CategoryGetById()
            {
                Id = x.ID,
                Name = x.Name
            });
            return Ok(category);
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category =await  _catogory.GetById(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateUpdate request)
        {
            var categoryId =await  _catogory.Create(request);
            if (categoryId == 0) return BadRequest("Create error");
            var category =await  _catogory.GetById(categoryId);
            return CreatedAtAction(nameof(GetById),new {Id = categoryId}, category);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm] CategoryCreateUpdate request)
        {
            var categoryId = await _catogory.Update(id,request);
            if (categoryId == 0) return BadRequest("update error");
            var category = await _catogory.GetById(id);
            return Ok(category);
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryId = await _catogory.Delete(id);
            if (categoryId==false) return BadRequest("delete error");
            return Ok();
        }
    }
}
