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
		public void AddProductToBasket(int productId, int userId, int amount);
		public void DeleteProductFromBasket(int productId, int userId);
		public void ChangeProductAmountInBasket(int productId, int userId, int amount);
		public IEnumerable<BasketPositionResponseDto>? GetUserBasket(int userId);
		
	}
}
