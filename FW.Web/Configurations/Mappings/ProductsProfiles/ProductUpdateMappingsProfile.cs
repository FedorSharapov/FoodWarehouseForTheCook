using AutoMapper;
using FW.BusinessLogic.Contracts.Products;
using FW.Web.ViewModels.Products;

namespace FW.Web.Configurations.Mappings.ProductsProfiles
{
    public class ProductUpdateMappingsProfile : Profile
    {
        public ProductUpdateMappingsProfile()
        {
            CreateMap<ProductUpdateDto, ProductVM>();
            CreateMap<ProductVM, ProductUpdateDto>()
                .ForMember(p => p.Id, map => map.Ignore())
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}
