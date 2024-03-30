using BLL.Dto.Order;
using BLL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public ActionResult<int> AddOrder([FromBody] int userId)
        {
            try
            {
                var order = _orderRepository.AddOrder(userId);
                return Ok(order);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderResponseDto>> GetAll()
        {
            return Ok(_orderRepository.GetAll());
        }


        [HttpGet("users/{userId}")]
        public ActionResult<IEnumerable<OrderResponseDto>> GetAllUserOrders(int userId)
        {
            try
            {
                var orders = _orderRepository.GetAllUserOrders(userId);
                return Ok(orders);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("orders/{orderId}")]
        public ActionResult<OrderResponseDto> GetOrder(int orderId)
        {
            try
            {
                var order = _orderRepository.GetOrder(orderId);
                return Ok(order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
