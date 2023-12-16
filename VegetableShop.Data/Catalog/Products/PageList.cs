using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegetableShop.Data.Catalog.Products
{
    public class PageList<T>:List<T>
    {
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public PageList(List<T> items, int count,int pageIndex,int pagesize)
        {
            PageIndex = pageIndex;
            TotalCount = (int)Math.Ceiling(count/(double)pagesize);
            AddRange(items);
        }
        public static PageList<T> Create(IQueryable<T> query ,int pageIndex,int pageSize)
        {
            var count = query.Count();
            var items = query.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return new PageList<T>(items,count,pageIndex,pageSize);
        }
    }
}
