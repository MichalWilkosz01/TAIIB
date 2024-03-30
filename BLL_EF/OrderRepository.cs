using AutoMapper;
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
		private readonly IMapper _mapper;

        public OrderRepository(WebshopContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

		public int AddOrder(int userId)
		{
			var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId) ??
                throw new InvalidOperationException($"User with ID {userId} not found.");

			var basketPositions = user.BasketPositions?.ToList() ??
                throw new InvalidOperationException("Cannot create order with no basket positions");

			var order = new Order()
			{
				User = user,
				Date = DateTime.Now
			};

			var orderPositions = basketPositions.Select(bp => new OrderPosition
			{
				Order = order,
				Product = bp.Product,
				Amount = bp.Amount,
				Price = bp.Amount * bp.Product.Price
			}).ToList();

			if (orderPositions.Any())
			{
				order.OrderPositions = orderPositions;
				basketPositions = null;
				_dbContext.Add(order);
				_dbContext.SaveChanges();
				return order.OrderId;
			}
			

            throw new InvalidOperationException("Cannot create order with no order positions");
        }

		public IEnumerable<OrderResponseDto> GetAll()
		{
			var orders = _dbContext.Orders.ToList();

			var ordersResponseDto = orders.Select(o => _mapper.Map<OrderResponseDto>(o));

			return ordersResponseDto;
		}

		public IEnumerable<OrderResponseDto> GetAllUserOrders(int userId)
		{
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId) ??
                throw new InvalidOperationException($"User with ID {userId} not found.");

			var allOrders = user.Orders?.ToList() ??
				throw new InvalidOperationException($"User with ID {userId} has no orders");

			return _mapper.Map<List<OrderResponseDto>>(allOrders);
        }

		public OrderResponseDto GetOrder(int orderId)
		{
			var order = _dbContext.Orders.FirstOrDefault(x => x.OrderId == orderId) ??
                throw new InvalidOperationException($"Order with ID {orderId} not found.");
            
			return _mapper.Map<OrderResponseDto>(order);
			//var orderPositions = order.OrderPositions.Select(op => new OrderPositionResponseDto
			//{
			//	Amount = op.Amount,
			//	Price = op.Price,
			//	Product = new ProductResponseDto
			//	{
			//		ProductId = op.ProductId,
			//		Name = op.Product.Name,
			//		Price = op.Product.Price,
			//		Image = op.Product.Image,
			//		IsActive = op.Product.IsActive
			//	}
			//}).ToList();

			//return new OrderResponseDto()
			//{
			//	Date = order.Date,
			//	OrderId = orderId,
			//	OrderPositions = orderPositions
			//};
		}

		//private IEnumerable<OrderResponseDto> OrdersToDtos(IEnumerable<Order> orders)
		//{
		//	List<OrderResponseDto> dtos = new List<OrderResponseDto>();
		//	foreach (var order in orders)
		//	{
		//		var orderDto = new OrderResponseDto()
		//		{
		//			OrderId = order.OrderId,
		//			Date = order.Date,
		//			OrderPositions = order.OrderPositions.Select(op => new OrderPositionResponseDto
		//			{
		//				Amount = op.Amount,
		//				Price = op.Price,
		//				Product = new ProductResponseDto
		//				{
		//					ProductId = op.ProductId,
		//					Name = op.Product.Name,
		//					Price = op.Product.Price,
		//					Image = op.Product.Image,
		//					IsActive = op.Product.IsActive
		//				}
		//			}).ToList()
		//		};

		//		dtos.Add(orderDto);
		//	}

		//	return dtos;
		//}

	}
}
