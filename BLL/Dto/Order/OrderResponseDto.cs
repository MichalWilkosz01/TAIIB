using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Order
{
	public class OrderResponseDto
	{
        public int OrderId { get; set; }
        public DateTime Date { get; set; }

		public IEnumerable<OrderPositionResponseDto>? OrderPositions { get; set; }
	}
}
