using BLL.Dto.Order;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
	public interface IOrderRepository
	{
		//public OrderResponseDto GetUserOrder(int userId, int orderId);
		public int AddOrder(int userId);
		public IEnumerable<Order> GetAll();
		public IEnumerable<Order> GetAllUserOrders(int userId);
		public OrderResponseDto GetOrder(int orderId);
	}
}
