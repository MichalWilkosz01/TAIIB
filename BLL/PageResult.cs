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
        public PageResult(List<T> items, int pageNumber, int pageSize)
        {
            Count = items.Count;
            Items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            PageIndex = pageNumber;
            PageSize = pageSize;
            TotalPages = (items.Count + PageSize - 1) / pageSize;
        }
    }
}
