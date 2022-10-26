using AutoMapper;
using FW.BusinessLogic.Contracts.Dishes;
using FW.Web.ViewModels.Dishes;

namespace FW.Web.Configurations.Mappings.DishesProfiles
{
    public class DishCreateMappingsProfile : Profile
    {
        public DishCreateMappingsProfile()
        {
            CreateMap<DishCreateDto, DishVM>();
            CreateMap<DishVM, DishCreateDto>()
                 .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}
