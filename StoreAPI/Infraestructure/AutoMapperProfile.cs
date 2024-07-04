using AutoMapper;
using ERP;
using ERP.Api.Models;
using System.Security.Claims;

namespace MinimalAPIERP.Infrastructure.Automapper
{   
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeo de Category a CategoryDto
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name));

            // Mapeo de Store a StoreDto
            CreateMap<Store, StoreDto>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name));

            // Mapeo de Product a ProductDto
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new CategoryDto { name = src.Category.name }));

            // Ejemplo ficticio: Mapeo de Class a ClassDto (ajustar según tus entidades y DTOs reales)
            CreateMap<Raincheck, RaincheckDto>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store.name))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.Name));
        }
    }
}
        