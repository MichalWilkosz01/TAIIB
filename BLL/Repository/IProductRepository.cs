using BLL.Dto.Product;
using BLL.Enums;
using BLL.Query;

namespace BLL.Repository
{
	public interface IProductRepository
	{
		public IEnumerable<ProductResponseDto> GetProducts(ProductQuery productQuery);
		public ProductResponseDto GetProduct(int id);
		public int AddProduct(ProductRequestDto dto);
		public void UpdateProduct(ProductRequestDto dto, int id);
		public void DeleteProduct(int id);
		public void SetProductActivity(int id);
	}
}
