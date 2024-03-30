using AutoMapper;
using BLL.Dto.Order;
using BLL.Dto.Product;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //Products
            CreateMap<ProductRequestDto, Product>();

            CreateMap<Product, ProductResponseDto>();

            CreateMap<ProductEditRequestDto, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //Baskets
            CreateMap<BasketPosition, OrderPosition>();
            CreateMap<BasketPosition, ProductResponseDto>();

            //Orders
            CreateMap<Order, OrderResponseDto>();
            CreateMap<OrderPosition, OrderPositionResponseDto>(); 
            
        }
    }
}
