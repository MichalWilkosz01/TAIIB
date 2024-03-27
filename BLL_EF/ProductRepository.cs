using BLL.Dto.Product;
using BLL.Query;
using BLL.Repository;
using DAL.Context;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_EF
{
	public class ProductRepository : IProductRepository
	{
		private readonly WebshopContext _context;

		public ProductRepository(WebshopContext context)
		{
			_context = context;
		}

		public int AddProduct(ProductRequestDto dto)
		{
			if (dto == null) throw new ArgumentNullException();
			if (dto.Price <= 0) throw new ArgumentOutOfRangeException();
			Product product = new Product()
			{
				Name = dto.Name,
				Price = dto.Price,
				Image = dto.Image,
			};

			if (dto.IsActive != null)
				product.IsActive = true;

			_context.Add(product);
			_context.SaveChanges();
			return product.ProductId;
		}

		public void DeleteProduct(int id)
		{
			throw new NotImplementedException();
		}

		public ProductResponseDto GetProduct(int id)
		{
			var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
			ProductResponseDto result = new ProductResponseDto()
			{
				Image = product.Image,
				Name = product.Name,
				Price = product.Price,
				IsActive = product.IsActive
			};
			return result;
		}

		public IEnumerable<ProductResponseDto> GetProducts(ProductQuery productQuery)
		{
			throw new NotImplementedException();
		}

		public void SetProductActivity(int id)
		{
			var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
			if (product.IsActive == true)
				throw new Exception();
			product.IsActive = true;
			_context.SaveChanges();
		}

		public void UpdateProduct(ProductRequestDto dto, int id)
		{
			throw new NotImplementedException();
		}
	}
}
