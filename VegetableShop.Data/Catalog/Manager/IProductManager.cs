using VegetableShop.WebApp.Models.Products;

namespace VegetableShop.Data.Catalog.Manager
{
    public interface IProductManager
    {
        Task<ProductGetById> GetById(int Id);
        Task<int> CreateProdcut(ProductCreateUpdate request);
        Task<int> UpdateProdcut(int Id, ProductCreateUpdate request);
        Task<bool> DeleteProduct(int Id);
    }
}
