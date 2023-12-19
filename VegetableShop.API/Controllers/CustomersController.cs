using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VegetableShop.Data.Catalog.Customers;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.EF;

namespace VegetableShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly VegetableShopDbContext _context;
        private readonly ICustomerManager _customerManager;

        public CustomersController(VegetableShopDbContext context, ICustomerManager customerManager)
        {
            _context = context;
            _customerManager = customerManager;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var customer = _context.Customers.Select(x => new CustomerGetById()
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Address = x.Address,
                NumberPhone = x.NumberPhone,
            });
            return Ok(customer);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            var customer = await _customerManager.GetById(Id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CustomerCreate request)
        {
            var customerId = await _customerManager.Create(request);
            if (customerId == 0) return BadRequest("Create error");
            return Ok();
        }
        [HttpPut("Id")]
        public async Task<IActionResult> Update(string Id, [FromForm] CustomerUpdate request)
        {
            var customerId = await _customerManager.UpdateCustomer(Id, request);
            if (customerId == 0) return BadRequest("Update error");
            return Ok();
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var billId = await _customerManager.DeleteById(Id);
            if (billId == false) return BadRequest("Delete error");
            return Ok();
        }
        [HttpPost("Authencate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authencate([FromForm] CustomerLogin request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var token = await _customerManager.Authencate(request);
            if (token == null) return BadRequest("error login");
            return Ok(token);
        }
    }
}
