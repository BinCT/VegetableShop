using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegetableShop.Data.Catalog.Customers;
using VegetableShop.Data.EF;
using VegetableShop.Data.Entitis;
using VegetableShop.Data.Password;

namespace VegetableShop.Data.Catalog.Manager
{
    public class CustomerManager : ICustomerManager
    {
        private readonly VegetableShopDbContext _context;
        public CustomerManager(VegetableShopDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Login(CustomerLogin request)
        {
			var customer =await  _context.Customers.Where(x => x.UserName == request.UserName).FirstOrDefaultAsync();
			if (customer == null || !HashAndVerify.VerifyHashedPassword(customer.Password, request.Password)) return false;
            return true;
        }
        public async Task<CustomerGetById> GetById(string id)
        {
            var customer = await _context.Customers.FindAsync(Guid.Parse(id));
            if (customer == null) throw new NotImplementedException("khong tim thay customer");
            var result = new CustomerGetById()
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Address = customer.Address,
                NumberPhone = customer.NumberPhone,

            };
            return result;
        }
        public async Task<int> Create(CustomerCreate request)
        {
            var customer =  _context.Customers.Where(x=>x.UserName  == request.UserName).FirstOrDefault();
            if (customer != null) throw new NotImplementedException("userName da ton tai");
            var result = new Customer()
            {
                Name = request.UserName,
                Email = request.Email,
                Address = request.Address,
                NumberPhone = request.NumberPhone,
                UserName = request.UserName,
                Password = HashAndVerify.HashPassword(request.Password),
                Remember = request.Remember
            };
            _context.Customers.Add(result);
            return await _context.SaveChangesAsync();

        }
        public async Task<int> UpdateCustomer(string id, CustomerUpdate request)
        {
            var customer = await _context.Customers.FindAsync(Guid.Parse(id));
            if (customer == null) throw new NotImplementedException("khong ton tai customer");
            customer.Name = request.Name;
            customer.Email = request.Email;
            customer.Address = request.Address;
            customer.NumberPhone = request.NumberPhone;
            _context.Customers.Update(customer);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteById(string id)
        {
            var customer = await _context.Customers.FindAsync(Guid.Parse(id));
            if (customer == null) throw new NotImplementedException("khong ton tai customer");
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> UpdateForgotPassword(string username, string email, CustomerForgotPassword request)
        {
            var customer = await _context.Customers.FindAsync(username);
            if (customer == null) throw new NotImplementedException("userName khong ton tai");
            if (customer.Email != email) throw new NotImplementedException("email khong dung");
            customer.Password = HashAndVerify.HashPassword(request.Password);
            _context.Customers.Update(customer);
            return await _context.SaveChangesAsync();
        }
    }
}
