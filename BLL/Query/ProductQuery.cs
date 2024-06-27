using BLL.Enums;

namespace BLL.Query
{
    public class ProductQuery
	{
        public string? Name { get; set; }
        public bool? IsActive { get; set; } = true;
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public SortDirectionEnum? SortDirection { get; set; }
    }
}
