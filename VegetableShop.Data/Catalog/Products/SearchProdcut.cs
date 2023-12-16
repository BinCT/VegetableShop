using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegetableShop.Data.Catalog.Products
{
    public class SearchProdcut
    {
        public string Keyword { get; set; }
        public decimal? From { get; set; }
        public decimal? To { get; set; }
        public string sortBy { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 2;

    }
}
