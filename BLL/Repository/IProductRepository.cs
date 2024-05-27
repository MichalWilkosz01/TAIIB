using BLL.Dto.Product;
using BLL.Enums;
using BLL.Query;

namespace BLL.Repository
{
	public interface IProductRepository
	{
		public PageResult<ProductResponseDto>? GetProducts(ProductQuery productQuery);
		public ProductResponseDto GetProduct(int id);
		public int AddProduct(ProductRequestDto dto);
		public void UpdateProduct(ProductEditRequestDto dto, int id);
		public void DeleteProduct(int id);
		public void SetProductActivity(int id);
	}
}
