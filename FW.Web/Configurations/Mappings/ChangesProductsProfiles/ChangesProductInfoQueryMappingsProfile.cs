using AutoMapper;
using FW.BusinessLogic.Contracts.ChangesProducts;
using FW.Web.ViewModels.ChangesProducts;

namespace FW.Web.Configurations.Mappings.ChangesProductsProfiles
{
    public class ChangesProductInfoQueryMappingsProfile : Profile
    {
        public ChangesProductInfoQueryMappingsProfile()
        {
            CreateMap<ChangesProductResponseVM, ChangesProductResponseDto>();
            CreateMap<ChangesProductResponseDto, ChangesProductResponseVM>();
        }
    }
}
