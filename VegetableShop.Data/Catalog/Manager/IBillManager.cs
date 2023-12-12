using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegetableShop.Data.Catalog.Bill;

namespace VegetableShop.Data.Catalog.Manager
{
    public interface IBillManager
    {
        Task<BillGetById> GetById(int Id);
        Task<int> CreateBill(BillCreateUpdate request);
        Task<int> UpdateBill(int Id, BillCreateUpdate request);
        Task<bool> DeleteBill(int Id);
    }
}
