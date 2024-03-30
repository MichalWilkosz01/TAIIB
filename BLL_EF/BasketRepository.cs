using BLL.Dto.Basket;
using BLL.Repository;
using DAL.Context;
using Model;
using BLL.Dto.Product;
using AutoMapper;

namespace BLL_EF
{
	public class BasketRepository : IBasketRepository
	{
		private readonly WebshopContext _dbContext;
		private readonly IMapper _mapper;
        public BasketRepository(WebshopContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public void AddProductToBasket(ProductToBasketDto dto)
		{
            if (dto.Amount <= 0)
			{
				throw new InvalidOperationException($"Amount cannot be lower or eqaul to 0");
			}

            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == dto.UserId) ??
                throw new InvalidOperationException($"User with ID {dto.UserId} not found.");

			var product = _dbContext.Products.FirstOrDefault(x=> x.ProductId == dto.ProductId) ??
                throw new InvalidOperationException($"Product with ID {dto.ProductId} not found.");

			if (!product.IsActive)
			{
				throw new InvalidOperationException("Product is unactive");

            }

			BasketPosition basketPosition = new BasketPosition() {
				Product = product,
				User = user,
				Amount = dto.Amount
            };
			_dbContext.BasketPositions.Add(basketPosition);
			_dbContext.SaveChanges();
		}

		public void ChangeProductAmountInBasket(ProductToBasketDto dto)
		{
            if (dto.Amount <= 0)
            {
                throw new InvalidOperationException($"Amount cannot be lower or eqaul to 0");
            }

            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == dto.UserId) ??
                throw new InvalidOperationException($"User with ID {dto.UserId} not found.");

            var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == dto.ProductId) ??
                throw new InvalidOperationException($"Product with ID {dto.ProductId} not found.");

            var basketPosition = user.BasketPositions?.FirstOrDefault(x => x.ProductId == dto.ProductId) ?? 
				throw new InvalidOperationException($"Basket position with product ID {dto.ProductId} not found.");

            basketPosition.Amount = dto.Amount;
			_dbContext.SaveChanges();
		}

		public void DeleteProductFromBasket(DeleteProductFromBasketDto dto)
		{
            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == dto.UserId) ??
				throw new InvalidOperationException($"User with ID {dto.UserId} not found.");

            var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == dto.ProductId) ??
                throw new InvalidOperationException($"Product with ID {dto.ProductId} not found.");

            var basketPosition = user.BasketPositions?.FirstOrDefault(x => x.ProductId == dto.ProductId) ??
				throw new InvalidOperationException($"Basket position with product ID {dto.ProductId} not found.");

            _dbContext.Remove(basketPosition);
			_dbContext.SaveChanges();
		}

		public IEnumerable<BasketPositionResponseDto>? GetUserBasket(int userId)
		{
            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == userId) ??
                throw new InvalidOperationException($"User with ID {userId} not found.");

			var userBasketList = user.BasketPositions?.ToList();

			if (userBasketList is null || !userBasketList.Any())
				return Enumerable.Empty<BasketPositionResponseDto>();


			List<BasketPositionResponseDto> responseDtos = new List<BasketPositionResponseDto>();
			_mapper.Map<List<BasketPositionResponseDto>>(userBasketList);


            return responseDtos;
		}

	}
}
