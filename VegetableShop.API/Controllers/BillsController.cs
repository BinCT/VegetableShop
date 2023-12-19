using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VegetableShop.Data.Catalog.Bill;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.EF;
using VegetableShop.Data.Entitis;

namespace VegetableShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly VegetableShopDbContext _context;
        private readonly IBillManager _billManager;

        public BillsController(VegetableShopDbContext context, IBillManager billManager)
        {
            _context = context;
            _billManager = billManager;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var bill = _context.Bills.Select(x=>new BillGetById()
            {
                ID = x.ID,
                CustomerId = x.CustomerId,
                CreateDay = x.CreateDay
            });
            return Ok(bill);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var bill =await _billManager.GetById(Id);
            if (bill == null) return NotFound();
            return Ok(bill);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]BillCreateUpdate request)
        {
            var billId =await  _billManager.CreateBill(request);
            if(billId == 0) return BadRequest("Create error");
            var bill =await _billManager.GetById(billId);
            return CreatedAtAction(nameof(GetById), new { Id = billId }, bill);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id,[FromForm] BillCreateUpdate request)
        {
            var billId = await _billManager.UpdateBill(Id,request);
            if (billId == 0) return BadRequest("Update error");
            return Ok();
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var billId = await _billManager.DeleteBill(Id);
            if (billId == false) return BadRequest("Delete error");
            return Ok();
        }
    }
}
