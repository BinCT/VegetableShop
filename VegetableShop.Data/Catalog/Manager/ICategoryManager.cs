using VegetableShop.WebApp.Models.Categories;
using VegetableShop.WebApp.Models.Products;

namespace VegetableShop.Data.Catalog.Manager
{
    public interface ICategoryManager
    {
        Task<CategoryGetById> GetById(int Id);
        Task<int> Create(CategoryCreateUpdate request);
        Task<int> Update(int Id, CategoryCreateUpdate request);
        Task<bool> Delete(int Id);
    }
}
