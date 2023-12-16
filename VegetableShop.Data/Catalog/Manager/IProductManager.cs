using VegetableShop.Data.Catalog.Products;

namespace VegetableShop.Data.Catalog.Manager
{
	public interface IProductManager
    {

        List<ProductGetById> Search(SearchProdcut request);
		Task<ProductGetById> GetById(int Id);
        Task<int> CreateProdcut(ProductCreateUpdate request);
        Task<int> UpdateProdcut(int Id, ProductCreateUpdate request);
        Task<bool> DeleteProduct(int Id);
    }
}
