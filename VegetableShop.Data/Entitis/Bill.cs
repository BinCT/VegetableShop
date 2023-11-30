namespace VegetableShop.Data.Entitis
{
	public class Bill
	{
		public int ID { get; set; }
		public DateTime CreateDay { get; set; }
		public Guid CustomerId { get; set; }
		public Customer Customer { get; set; }
		public List<BillDetail>	 BillDetails { get; set; }
		
 	}
}
