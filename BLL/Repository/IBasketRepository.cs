using BLL.Dto.Basket;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
	public interface IBasketRepository
	{
		public void AddProductToBasket(ProductToBasketDto dto);
		public void DeleteProductFromBasket(DeleteProductFromBasketDto dto);
		public void ChangeProductAmountInBasket(ProductToBasketDto dto);
		public IEnumerable<BasketPositionResponseDto>? GetUserBasket(int userId);
		
	}
}
