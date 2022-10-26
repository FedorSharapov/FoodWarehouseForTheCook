using AutoMapper;
using FW.BusinessLogic.Contracts.Products;
using FW.Web.ViewModels.Products;

namespace FW.Web.Configurations.Mappings.ProductsProfiles
{
    /// <summary>
    /// Профиль автомаппера для сущности Продукты.
    /// </summary>
    public class ProductCreateMappingsProfile : Profile
    {
        public ProductCreateMappingsProfile()
        {
            CreateMap<ProductCreateDto, ProductVM>();
            CreateMap<ProductVM, ProductCreateDto>()
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}
