using AutoMapper;
using FW.BusinessLogic.Contracts.Products;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.ProductsProfiles
{
    /// <summary>
    /// Профиль автомаппера для сущности Продукты.
    /// </summary>
    public class ProductCreateMappingsProfile : Profile
    {
        public ProductCreateMappingsProfile()
        {
            CreateMap<Products, ProductCreateDto>();
            CreateMap<ProductCreateDto, Products>()
                .ForMember(p => p.Id, map => map.Ignore())
                .ForMember(p => p.ModifiedOn, map => map.Ignore())
                .ForMember(p => p.Warehouse, map => map.Ignore())
                .ForMember(p => p.Category, map => map.Ignore())
                .ForMember(p => p.Ingredients, map => map.Ignore());
        }
    }
}
