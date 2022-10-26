using AutoMapper;
using FW.BusinessLogic.Contracts.Dishes;
using FW.Web.ViewModels.Dishes;

namespace FW.Web.Configurations.Mappings.DishesProfiles
{
    public class DishInfoQueryMappingsProfile : Profile
    {
        public DishInfoQueryMappingsProfile()
        {
            CreateMap<DishResponseVM, DishResponseDto>();
            CreateMap<DishResponseDto, DishResponseVM>();
        }
    }
}
