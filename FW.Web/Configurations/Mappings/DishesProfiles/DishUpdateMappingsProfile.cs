using AutoMapper;
using FW.BusinessLogic.Contracts.Dishes;
using FW.Web.ViewModels.Dishes;

namespace FW.Web.Configurations.Mappings.DishesProfiles
{
    public class DishUpdateMappingsProfile : Profile
    {
        public DishUpdateMappingsProfile()
        {
            CreateMap<DishUpdateDto, DishVM>();
            CreateMap<DishVM, DishUpdateDto>()
                .ForMember(p => p.Id, map => map.Ignore())
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}
