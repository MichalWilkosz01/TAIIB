using BLL.Dto.Product;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Order
{
	public class OrderPositionResponseDto
	{
		public int Amount { get; set; }
		public decimal Price { get; set; }
		public ProductResponseDto Product { get; set; }
	}
}
