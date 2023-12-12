using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegetableShop.Data.Catalog.Bill
{
	public class BillGetById
	{
		public int ID { get; set; }
		public DateTime CreateDay { get; set; }
		public Guid CustomerId { get; set; }
	}
}
