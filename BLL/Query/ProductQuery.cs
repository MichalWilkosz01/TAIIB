using BLL.Enums;

namespace BLL.Query
{
    public class ProductQuery
	{
        public string? name { get; set; }
        public bool? isActive { get; set; } = true;
        public int page { get; set; }
        public int count { get; set; }
        public SortDirectionEnum? sortDirection { get; set; }
    }
}
