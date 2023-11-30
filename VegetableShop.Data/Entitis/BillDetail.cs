namespace VegetableShop.Data.Entitis
{
	public  class BillDetail
	{
		public int BillId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }

		public Bill Bill { get; set; }

		public Product Product { get; set; }
	}
}
