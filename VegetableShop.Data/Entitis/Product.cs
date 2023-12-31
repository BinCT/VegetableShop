﻿namespace VegetableShop.Data.Entitis
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set;}	
		public decimal Price { get; set; }
		public string FilePast { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }

		public List<BillDetail> BillDetails { get; set;}
	}
}
