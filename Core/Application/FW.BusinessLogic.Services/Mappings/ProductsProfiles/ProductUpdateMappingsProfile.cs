using AutoMapper;
using FW.BusinessLogic.Contracts.Products;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.ProductsProfiles
{
    public class ProductUpdateMappingsProfile : Profile
    {
        public ProductUpdateMappingsProfile()
        {
            CreateMap<Products, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, Products>()
                .ForMember(p => p.ModifiedOn, map => map.Ignore())
                .ForMember(p => p.Warehouse, map => map.Ignore())
                .ForMember(p => p.Category, map => map.Ignore())
                .ForMember(p => p.Ingredients, map => map.Ignore());
        }
    }
}
