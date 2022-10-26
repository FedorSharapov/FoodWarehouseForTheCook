using AutoMapper;
using FW.BusinessLogic.Contracts.Products;
using FW.Web.ViewModels.Products;

namespace FW.Web.Configurations.Mappings.ProductsProfiles
{
    public class ProductInfoQueryMappingsProfile : Profile
    {
        public ProductInfoQueryMappingsProfile()
        {
            CreateMap<ProductResponseVM, ProductResponceDto>();
            CreateMap<ProductResponceDto, ProductResponseVM>();
        }
    }
}
