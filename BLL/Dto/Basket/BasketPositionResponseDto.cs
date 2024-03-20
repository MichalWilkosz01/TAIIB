using BLL.Dto.Product;
using Model;

namespace BLL.Dto.Basket
{
	public class BasketPositionResponseDto
	{      
		public ProductResponseDto Product { get; set; }
		public int Amount { get; set; }
	}
}
