using BLL.Dto.Product;
using BLL.Query;
using BLL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductResponseDto>> GetProducts([FromQuery] ProductQuery productQuery)
        {
            try
            {
                var sortedProducts = _productRepository.GetProducts(productQuery);
                return Ok(sortedProducts);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        [HttpGet("{id}")]
        public ActionResult<ProductResponseDto> GetProduct(int id)
        {
            try
            {
                var product = _productRepository.GetProduct(id);
                return product;
            }
            catch(InvalidOperationException ex) 
            {
                return BadRequest(ex.Message);
            }           
        }

        [HttpPost]
        public ActionResult<int> AddProduct([FromBody] ProductRequestDto dto)
        {
            try
            {
                var result = _productRepository.AddProduct(dto);
                return Ok(result);
            }
            catch(ArgumentNullException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct([FromBody] ProductEditRequestDto dto, int id)
        {
            try
            {
                _productRepository.UpdateProduct(dto, id);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                _productRepository.DeleteProduct(id);
                return NoContent();
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}/activity")]
        public ActionResult SetProductActivity(int id)
        {
            try
            {
                _productRepository.SetProductActivity(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
