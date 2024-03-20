using BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Query
{
	public class ProductQuery
	{
		string? name;
		bool? isActive;
		int page;
		int count;
		SortDirectionEnum? sortDirection;
	}
}
