
using System.Net.Http.Headers;
using VegetableShop.Data.EF;
using VegetableShop.Data.Entitis;
using VegetableShop.Data.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VegetableShop.WebApp.Models.Products;

namespace VegetableShop.Data.Catalog.Manager
{
    public class ProductManager : IProductManager
    {
        private readonly VegetableShopDbContext _context;
        private readonly IStorageService _storageService;
        public ProductManager(VegetableShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<ProductGetById> GetById(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            if (product == null) throw new NotImplementedException("Khong tin thay product");
            var result = new ProductGetById()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Quantity = product.Quantity,
                Price = product.Price,
                CategoryId = product.CategoryId,
                FilePast = product.FilePast,
            };
            return result;
        }
        public async Task<int> CreateProdcut(ProductCreateUpdate request)
        {

            var result = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Quantity = request.Quantity,
                Price = request.Price,
                CategoryId = request.CategoryId,
            };
            if (request.FilePast != null)
            {
                result.FilePast = await SaveFile(request.FilePast);
            }
            _context.Products.Add(result);
            await _context.SaveChangesAsync();
            return result.Id;
        }
        public async Task<int> UpdateProdcut(int Id, ProductCreateUpdate request)
        {
            var product = await _context.Products.FindAsync(Id);
            if (product == null) throw new NotImplementedException("Khong tin thay product");
            product.Name = request.Name;
            product.Description = request.Description;
            product.Quantity = request.Quantity;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;
            if (request.FilePast != null)
            {
                await _storageService.DeleteFileAsync(product.FilePast);
                product.FilePast = await SaveFile(request.FilePast);
            }
            _context.Products.Update(product);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteProduct(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            if (product != null)
            {
                await _storageService.DeleteFileAsync(product.FilePast);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
