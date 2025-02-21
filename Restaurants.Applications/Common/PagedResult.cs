using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Applications.Common
{
    public class PagedResult<T> where T : class
    {
        public PagedResult(IEnumerable<T> items, int totalItemsCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalPages = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
            TotalItemsCount = totalItemsCount;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = Math.Min(totalItemsCount, ItemsFrom + pageSize - 1);
        }

        public IEnumerable<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalItemsCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
    }
}
