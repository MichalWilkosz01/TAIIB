using BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PageResult<T>
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }
        public PageResult(List<T> items, int pageNumber, int pageSize, SortDirectionEnum? sortDirection, Func<T, object>? sortKeySelector)
        {
            var itemsToSort = new List<T>(items);
            if (sortDirection.HasValue && sortKeySelector is not null)
            {
                itemsToSort = sortDirection.Value == SortDirectionEnum.Ascending
                    ? itemsToSort.OrderBy(sortKeySelector).ToList()
                    : itemsToSort.OrderByDescending(sortKeySelector).ToList();

            }
            Count = itemsToSort.Count;
            Items = itemsToSort.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            PageIndex = pageNumber;
            PageSize = pageSize;
            TotalPages = (itemsToSort.Count + PageSize - 1) / pageSize;
            
        }
    }
}
