using AutoMapper;
using Lab2_3.Bussiness.DTO;
using Lab2_3.DataAccess.Models;

namespace Lab2_3.Bussiness.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<OrderDetailDTO, OrderDetail>();
            CreateMap<OrderDTO, Order>();
        }
    }
}
