using AutoMapper;
using FW.BusinessLogic.Contracts.ChangesProducts;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.ChangesProductsProfiles
{
    public class ChangesProductInfoResponseMappingsProfile : Profile
    {
        public ChangesProductInfoResponseMappingsProfile()
        {
            CreateMap<ChangesProducts, ChangesProductResponseDto>();
            CreateMap<ChangesProductResponseDto, ChangesProducts>()
                .ForMember(p => p.Products, map => map.Ignore())
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}