using AutoMapper;
using FW.BusinessLogic.Contracts.Products;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.ProductsProfiles
{
    public class ProductInfoResponseMappingsProfile : Profile
    {
        public ProductInfoResponseMappingsProfile()
        {
            CreateMap<Products, ProductResponceDto>();
            CreateMap<ProductResponceDto, Products>()
                .ForMember(p => p.Warehouse, map => map.Ignore())
                .ForMember(p => p.Category, map => map.Ignore())
                .ForMember(p => p.Ingredients, map => map.Ignore())
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}
