using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegetableShop.Data.Catalog.Customers;

namespace VegetableShop.Data.Catalog.Manager
{
    public interface ICustomerManager
    {
        Task<bool> Login(CustomerLogin request);
        Task<CustomerGetById> GetById(string id);
        Task<int> Create(CustomerCreate request);
        Task<int> UpdateCustomer(string Id, CustomerUpdate request);
        Task<bool> DeleteById(string id);
        Task<int> UpdateForgotPassword(string username, string email, CustomerForgotPassword request);
        Task<string> Authencate(CustomerLogin request);
    }
}
