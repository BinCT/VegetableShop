using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VegetableShop.Data.Catalog.Bill;
using VegetableShop.Data.Catalog.BillDetail;
using VegetableShop.Data.Catalog.Manager;
using VegetableShop.Data.EF;

namespace VegetableShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailsController : ControllerBase
    {
        private readonly VegetableShopDbContext _context;
        private readonly IBillDetailManager _billdetailManager;

        public BillDetailsController(VegetableShopDbContext context, IBillDetailManager billdetailManager)
        {
            _context = context;
            _billdetailManager = billdetailManager;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var billdetail = _context.BillDetails.Select(x => new BillDetailGetById()
            {
                BillId = x.BillId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
            });
            return Ok(billdetail);
        }
        [HttpGet("{BillId}/{ProductId}")]
        public async Task<IActionResult> GetById(int BillId, int ProductId)
        {
            var billdetail = await _billdetailManager.GetById(BillId, ProductId);
            if (billdetail == null) return NotFound();
            return Ok(billdetail);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BillDetailCreateUpdate request)
        {
            var billdetailId = await _billdetailManager.Create(request);
            if (billdetailId == 0) return BadRequest("Create error");
            var billdetail = await _billdetailManager.GetById(billdetailId, request.ProductId);
            return CreatedAtAction(nameof(GetById), new { BillId = billdetailId,ProductId = request.ProductId }, billdetail);
        }
        [HttpPut("{BillId}/{ProductId}")]
        public async Task<IActionResult> Update(int BillId, int ProductId, [FromForm] BillDetailCreateUpdate request)
        {
            var billdetailId = await _billdetailManager.Update(BillId, ProductId, request);
            if (billdetailId == 0) return BadRequest("Update error");
            return Ok();
        }
        [HttpDelete("{BillId}/{ProductId}")]
        public async Task<IActionResult> Delete(int BillId, int ProductId)
        {
            var billId = await _billdetailManager.Delete(BillId, ProductId);
            if (billId == false) return BadRequest("Delete error");
            return Ok();
        }
    }
}
