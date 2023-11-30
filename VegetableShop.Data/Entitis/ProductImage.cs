namespace VegetableShop.Data.Entitis
{
	public  class ProductImage
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string FilePast { get; set; }
		public string FileSize { get; set; }
		public DateTime DatetimeCreate { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
