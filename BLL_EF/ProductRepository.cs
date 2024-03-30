using AutoMapper;
using BLL.Dto.Product;
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

		public IEnumerable<ProductResponseDto>? GetProducts(ProductQuery productQuery)
		{
			List<Product> products = new List<Product>();
			if (productQuery.name is not null)
			{
				products = _context.Products.Where(x => x.Name.Contains(productQuery.name)).ToList();
				var productsResult = _mapper.Map<IEnumerable<ProductResponseDto>>(products);

                if (productsResult.Any())
                {
                    products = products.Where(x => x.IsActive == productQuery.isActive).ToList();
                    return productsResult;
                }
            }

			return Enumerable.Empty<ProductResponseDto>();
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
