﻿namespace VegetableShop.WebApp.Models.Products
{
	public class ProductGetById
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public string FilePast { get; set; }

		public int CategoryId { get; set; }
	}
}