using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        private readonly IConfiguration _configuration;
        public CustomerManager(VegetableShopDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

        public async Task<string> Authencate(CustomerLogin request)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (customer == null || !HashAndVerify.VerifyHashedPassword(customer.Password, request.Password))
            {
                return null;
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, customer.UserName),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim(ClaimTypes.MobilePhone, customer.NumberPhone),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var haskey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: haskey
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
