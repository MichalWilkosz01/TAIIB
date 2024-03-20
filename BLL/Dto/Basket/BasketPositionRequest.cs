using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Basket
{
	public class BasketPositionRequest
	{
		public int ProductId { get; set; }
		public int UserId { get; set; }
		public int Amount { get; set; }
	}
}
