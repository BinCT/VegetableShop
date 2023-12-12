using VegetableShop.Data.Catalog.Bill;
using VegetableShop.Data.EF;

namespace VegetableShop.Data.Catalog.Manager
{
    public class BillManager : IBillManager
    {
        private readonly VegetableShopDbContext _context;

        public BillManager(VegetableShopDbContext context)
        {
            _context = context;
        }
        public async Task<BillGetById> GetById(int Id)
        {
            var bill = await _context.Bills.FindAsync(Id);
            if (bill == null) throw new NotImplementedException("khong tim thay Bill");
            var result = new BillGetById()
            {
                ID = bill.ID,
                CreateDay = bill.CreateDay,
                CustomerId = bill.CustomerId,
            };
            return result;
        }
        public async Task<int> CreateBill(BillCreateUpdate request)
        {
            var bill = new Entitis.Bill()
            {
                CustomerId = request.CustomerId,
                CreateDay = DateTime.Now
            };
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
            return bill.ID;
            //throw new NotImplementedException();
        }
        public async Task<int> UpdateBill(int Id, BillCreateUpdate request)
        {
            var bill = await _context.Bills.FindAsync(Id);
            if (bill == null) throw new NotImplementedException("khong tim thay bill");
            bill.CustomerId = request.CustomerId;
            _context.Bills.Update(bill);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteBill(int Id)
        {
            var bill = await _context.Bills.FindAsync(Id);
            if (bill == null) throw new NotImplementedException("khong tim thay bill");
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return true;
        }




    }
}
