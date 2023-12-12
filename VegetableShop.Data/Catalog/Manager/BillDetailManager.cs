using Azure.Core;
using VegetableShop.Data.Catalog.BillDetail;
using VegetableShop.Data.EF;
using VegetableShop.Data.Entitis;

namespace VegetableShop.Data.Catalog.Manager
{
	public class BillDetailManager : IBillDetailManager
    {
        private readonly VegetableShopDbContext _context;
        public BillDetailManager(VegetableShopDbContext context)
        {
            _context = context;
        }
        public async Task<BillDetailGetById> GetById(int billid,int productid )
        {
            var billdetail = await _context.BillDetails.FindAsync(billid, productid);
            if (billdetail == null) throw new NotImplementedException();
            var result = new BillDetailGetById()
            {
                BillId = billdetail.BillId,
                ProductId = billdetail.ProductId,
                Quantity = billdetail.Quantity,
            };
            return result;
        }
        public async Task<int> Create(BillDetailCreateUpdate request)
        {
            var result = await _context.BillDetails.FindAsync(request.BillId,request.ProductId);
            if(result == null)
            {
				var billDetail = new Entitis.BillDetail()
				{
					BillId = request.BillId,
					Quantity = request.Quantity,
					ProductId = request.ProductId,
				};
				_context.BillDetails.Add(billDetail);
				await _context.SaveChangesAsync();
				return billDetail.BillId;
			}
            else
            {
                result.Quantity++;
				_context.BillDetails.Update(result);
				await _context.SaveChangesAsync();
				return result.BillId;
			}
            
        }
        public async Task<int> Update(int billid, int productid, BillDetailCreateUpdate requet)
        {
            var billdetail = await _context.BillDetails.FindAsync(billid,productid);
            if (billdetail == null) throw new NotImplementedException();
			billdetail.BillId = requet.BillId;
			billdetail.Quantity = requet.Quantity;
            billdetail.ProductId = requet.ProductId;
			var result = await _context.BillDetails.FindAsync(billdetail.BillId, billdetail.ProductId);
            if(result == null)
            {
				_context.BillDetails.Update(billdetail);
				await _context.SaveChangesAsync();
				return billdetail.BillId;
            }
            else
            {
				throw new NotImplementedException();
			}
			
        }
        public async Task<bool> Delete(int billid, int productid)
        {
            var billdetail = await _context.BillDetails.FindAsync(billid, productid);
            if (billdetail == null) throw new NotImplementedException();
            _context.BillDetails.Remove(billdetail);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
