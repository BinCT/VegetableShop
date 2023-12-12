using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegetableShop.Data.Catalog.BillDetail;

namespace VegetableShop.Data.Catalog.Manager
{
    public interface IBillDetailManager
    {
        Task<BillDetailGetById> GetById(int billid, int productid);
        Task<int> Create(BillDetailCreateUpdate request);
        Task<int> Update(int billid, int productid, BillDetailCreateUpdate requet);
        Task<bool> Delete(int billid, int productid);
    }
}
