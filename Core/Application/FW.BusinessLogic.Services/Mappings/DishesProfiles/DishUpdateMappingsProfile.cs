using AutoMapper;
using FW.BusinessLogic.Contracts.Dishes;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.DishesProfiles
{
    public class DishUpdateMappingsProfile : Profile
    {
        public DishUpdateMappingsProfile()
        {
            CreateMap<Dishes, DishUpdateDto>();
            CreateMap<DishUpdateDto, Dishes>()
                .ForMember(p => p.ModifiedOn, map => map.Ignore()); 
        }
    }
}
