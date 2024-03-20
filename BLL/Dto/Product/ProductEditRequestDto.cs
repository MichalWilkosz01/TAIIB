using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Product
{
	public class ProductEditRequestDto
	{
		public int? ProductId { get; set; }
		public string? Name { get; set; }
		public decimal? Price { get; set; }
		public string? Image { get; set; }
		public bool? IsActive { get; set; }
	}
}
