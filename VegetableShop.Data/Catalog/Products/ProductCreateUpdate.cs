using Microsoft.AspNetCore.Http;

namespace VegetableShop.WebApp.Models.Products
{
	public class ProductCreateUpdate
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public IFormFile FilePast { get; set; }
		public int CategoryId { get; set; }
	}
}
