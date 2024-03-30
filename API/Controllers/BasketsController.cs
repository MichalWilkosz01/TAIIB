using BLL.Dto.Basket;
using BLL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketsController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpPost]
        public ActionResult AddProductToBasket([FromBody] ProductToBasketDto dto)
        {
            try
            {
                _basketRepository.AddProductToBasket(dto);
                return Ok();
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteProductFromBasket([FromBody] DeleteProductFromBasketDto dto)
        {
            try
            {
                _basketRepository.DeleteProductFromBasket(dto);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult ChangeProductAmountInBasket([FromBody] ProductToBasketDto dto)
        {
            try
            {
                _basketRepository.ChangeProductAmountInBasket(dto);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users/{userId}")]
        public ActionResult<IEnumerable<BasketPositionResponseDto>?> GetUserBasket(int userId)
        {
            try
            {
                var userBasket = _basketRepository.GetUserBasket(userId);
                return Ok(userBasket);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
