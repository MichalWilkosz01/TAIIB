using BLL.Dto.Order;
using BLL.Dto.Product;
using BLL.Repository;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Model;


namespace BLL_EF
{
	public class OrderRepository : IOrderRepository
	{
		private readonly WebshopContext _dbContext;

		public OrderRepository(WebshopContext dbContext)
		{
			_dbContext = dbContext;
		}

		public int AddOrder(int userId)
		{
			if(IfUserExists(userId))
			{
				var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
				var basketPositions = user.BasketPositions.ToList();
				List<OrderPosition> orders = new List<OrderPosition>();
				var order = new Order()
				{
					User = user,
					Date = DateTime.Now
				};
				foreach (var orderPosition in basketPositions)
				{
					orders.Add(new OrderPosition()
					{
						Order = order,
						Product = orderPosition.Product,
						Amount = orderPosition.Amount,
						Price = orderPosition.Product.Price
					});
				}
				//SPrawdz czy orders nie jest puste
				order.OrderPositions = orders;
				_dbContext.Add(order);
				_dbContext.SaveChanges();
				return order.OrderId;
			}
			else
			{
				throw new Exception();
			}
		}

		public IEnumerable<OrderResponseDto> GetAll()
		{
			var allUsers = _dbContext.Users.Include(u => u.Orders)
				.ThenInclude(o => o.OrderPositions).ToList();
			List<Order> allOrders = allUsers.SelectMany(u => u.Orders).ToList();

			return OrdersToDtos(allOrders);
		}

		public IEnumerable<OrderResponseDto> GetAllUserOrders(int userId)
		{
			if (IfUserExists(userId))
			{
				var user = _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
				List<Order> allOrders = user.Orders.ToList();
				return OrdersToDtos(allOrders);
			}
			else
			{
				throw new Exception();
			}
		}

		public OrderResponseDto GetOrder(int orderId)
		{
			var order = _dbContext.Orders.FirstOrDefault(x => x.OrderId == orderId);
			var orderPositions = order.OrderPositions.Select(op => new OrderPositionResponseDto
			{
				Amount = op.Amount,
				Price = op.Price,
				Product = new ProductResponseDto
				{
					ProductId = op.ProductId,
					Name = op.Product.Name,
					Price = op.Product.Price,
					Image = op.Product.Image,
					IsActive = op.Product.IsActive
				}
			}).ToList();

			return new OrderResponseDto()
			{
				Date = order.Date,
				OrderId = orderId,
				OrderPositions = orderPositions
			};
		}

		private bool IfUserExists(int userId)
		{
			return _dbContext.Users.Any(x => x.UserId == userId);
		}

		private IEnumerable<OrderResponseDto> OrdersToDtos(IEnumerable<Order> orders)
		{
			List<OrderResponseDto> dtos = new List<OrderResponseDto>();
			foreach (var order in orders)
			{
				var orderDto = new OrderResponseDto()
				{
					OrderId = order.OrderId,
					Date = order.Date,
					OrderPositions = order.OrderPositions.Select(op => new OrderPositionResponseDto
					{
						Amount = op.Amount,
						Price = op.Price,
						Product = new ProductResponseDto
						{
							ProductId = op.ProductId,
							Name = op.Product.Name,
							Price = op.Product.Price,
							Image = op.Product.Image,
							IsActive = op.Product.IsActive
						}
					}).ToList()
				};

				dtos.Add(orderDto);
			}

			return dtos;
		}

	}
}
