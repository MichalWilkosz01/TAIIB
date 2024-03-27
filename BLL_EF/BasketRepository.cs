using BLL.Dto.Basket;
using BLL.Repository;
using DAL.Context;
using Model;
using BLL.Dto.Product;

namespace BLL_EF
{
	public class BasketRepository : IBasketRepository
	{
		private readonly WebshopContext _dbContext;
        public BasketRepository(WebshopContext context)
        {
            _dbContext = context;
        }

        public void AddProductToBasket(int productId, int userId, int amount)
		{
			if(IfUserExists(userId) && IfProductExists(productId) && amount > 0)
			{
				var user = _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
				var produkt = _dbContext.Products.FirstOrDefault(x=> x.ProductId == productId);
				BasketPosition basketPosition = new BasketPosition() {
					Product = produkt,
					User = user,
					Amount = amount
				};
				_dbContext.BasketPositions.Add(basketPosition);
				_dbContext.SaveChanges();
			}
			else
			{
				throw new Exception();
			}
		}

		public void ChangeProductAmountInBasket(int productId, int userId, int amount)
		{
			if (IfUserExists(userId) && IfProductExists(productId) && amount > 0)
			{
				var basketPosition = GetBasketPosition(productId, userId);
				basketPosition.Amount = amount;
				_dbContext.SaveChanges();
			}
			else
			{
				throw new Exception();
			}
		}

		public void DeleteProductFromBasket(int productId, int userId)
		{
			if (IfUserExists(userId) && IfProductExists(productId))
			{
				var basketPosition = GetBasketPosition(productId, userId);
				_dbContext.Remove(basketPosition);
				_dbContext.SaveChanges();
			}
			else
			{
				throw new Exception();
			}
		}

		public IEnumerable<BasketPositionResponseDto>? GetUserBasket(int userId)
		{
			var user = _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
			if (user == null)
				throw new Exception();
			var userBasketList = user.BasketPositions?.ToList();
			List<BasketPositionResponseDto> responseDtos = new List<BasketPositionResponseDto>();
			foreach (var basketPosition in userBasketList)
			{
				responseDtos.Add(
					new BasketPositionResponseDto()
					{
						Product = new ProductResponseDto()
						{
							ProductId = basketPosition.ProductId,
							Name = basketPosition.Product.Name,
							Price = basketPosition.Product.Price,
							Image = basketPosition.Product.Image,
							IsActive = basketPosition.Product.IsActive
						}
					});
			}

			return responseDtos;
		}

		private bool IfUserExists(int userId)
		{
			return _dbContext.Users.Any(x => x.UserId == userId);
		}

		private bool IfProductExists(int productId)
		{
			return _dbContext.Products.Any(x => x.ProductId == productId);
		}

		private BasketPosition GetBasketPosition(int productId, int userId)
		{
			if(IfUserExists(userId) && IfProductExists(productId)) 
			{
				var user = _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
				var basketPosition = user.BasketPositions.FirstOrDefault(x => x.ProductId == productId);
				return basketPosition;
			}
			else
			{
				throw new Exception();
			}
		}
	}
}
