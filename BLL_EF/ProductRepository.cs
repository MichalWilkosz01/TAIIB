using AutoMapper;
using BLL;
using BLL.Dto.Product;
using BLL.Enums;
using BLL.Query;
using BLL.Repository;
using DAL.Context;
using Model;

namespace BLL_EF
{
	public class ProductRepository : IProductRepository
	{
		private readonly WebshopContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(WebshopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int AddProduct(ProductRequestDto dto)
		{
			if (dto == null) throw new ArgumentNullException();
			if (dto.Price <= 0) throw new ArgumentOutOfRangeException();
			Product product = _mapper.Map<Product>(dto);

			_context.Add(product);
			_context.SaveChanges();
			return product.ProductId;
		}

		public void DeleteProduct(int id)
		{
			var product = _context.Products.FirstOrDefault(x => x.ProductId == id) ??
				throw new InvalidOperationException($"Product with ID {id} not found.");

			if (product.BasketPositions == null || product.BasketPositions.Any())
			{
				throw new InvalidOperationException($"Product with ID {id} cannot be deleted because its included in basket");
			}

			if (product.OrderPositions != null || product.OrderPositions.Any())
			{
				product.IsActive = false;
			}
			else
			{
                _context.Remove(product);
            }
			
			_context.SaveChanges();
        }

		public ProductResponseDto GetProduct(int id)
		{
			var product = _mapper.Map<ProductResponseDto>(_context.Products.FirstOrDefault(x => x.ProductId == id)) ??
                throw new InvalidOperationException($"Product with ID {id} not found.");

   //         ProductResponseDto result = new ProductResponseDto()
			//{
			//	Image = product.Image,
			//	Name = product.Name,
			//	Price = product.Price,
			//	IsActive = product.IsActive
			//};
			return product;
		}

		public PageResult<ProductResponseDto>? GetProducts(ProductQuery productQuery)
		{
            IEnumerable<Product> products;
            IEnumerable<ProductResponseDto> productsResult;

            if (!string.IsNullOrEmpty(productQuery.Name))
            {
                products = _context.Products.Where(x => x.Name.Contains(productQuery.Name)).ToList();
            }
            else
            {
                products = _context.Products.ToList();
            }

            productsResult = _mapper.Map<IEnumerable<ProductResponseDto>>(products);

            if (productsResult.Any())
            {
                if (productQuery.Name != null)
                {
                    products = products.Where(x => x.IsActive == productQuery.IsActive).ToList();
                    productsResult = _mapper.Map<IEnumerable<ProductResponseDto>>(products);

                }
                Func<ProductResponseDto, object>? sortKeySelector = item => item.Price;
				var result = new PageResult<ProductResponseDto>(productsResult.ToList(), productQuery.PageIndex, productQuery.PageSize, productQuery.SortDirection, sortKeySelector);
				return result;
            }

            return new PageResult<ProductResponseDto>(productsResult.ToList(), productQuery.PageIndex, productQuery.PageSize, null, null);
        }

		public void SetProductActivity(int id)
		{
			var product = _context.Products.FirstOrDefault(x => x.ProductId == id) ??
                throw new InvalidOperationException($"Product with ID {id} not found.");

			if (product.IsActive == true)
                throw new InvalidOperationException($"Product with ID {id} is already active.");

            product.IsActive = true;
			_context.SaveChanges();
		}

		public void UpdateProduct(ProductEditRequestDto dto, int id)
		{
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id) ??
                throw new InvalidOperationException($"Product with ID {id} not found.");

            if (dto.Price <= 0) throw new ArgumentOutOfRangeException();
            _mapper.Map(dto, product);
			_context.SaveChanges();
        }
	}
}
