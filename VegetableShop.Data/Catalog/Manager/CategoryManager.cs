using VegetableShop.Data.EF;
using VegetableShop.Data.Entitis;
using VegetableShop.Data.Catalog.Categories;

namespace VegetableShop.Data.Catalog.Manager
{
    public class CategoryManager : ICategoryManager
    {
        private readonly VegetableShopDbContext _context;

        public CategoryManager(VegetableShopDbContext context)
        {
            _context = context;
        }
        public async Task<CategoryGetById> GetById(int Id)
        {
            var category = await _context.Categories.FindAsync(Id);
            if (category == null) throw new NotImplementedException("khong tim thay category");
            var result = new CategoryGetById()
            {
                Id = category.ID,
                Name = category.Name,
            };
            return result;
        }
        public async Task<int> Create(CategoryCreateUpdate request)
        {
            var result = new Category()
            {
                Name = request.Name,
            };
            _context.Categories.Add(result);
            await _context.SaveChangesAsync();
            return result.ID;
        }
        public async Task<int> Update(int Id, CategoryCreateUpdate request)
        {
            var category = await _context.Categories.FindAsync(Id);
            if (category == null) throw new NotImplementedException("khong tim thay category");
            category.Name = request.Name;
            _context.Categories.Update(category);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> Delete(int Id)
        {
            var category = await _context.Categories.FindAsync(Id);
            if (category == null) return false;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return false;
        }
    }
}
