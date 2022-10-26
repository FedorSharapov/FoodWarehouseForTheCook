using AutoMapper;
using FW.BusinessLogic.Contracts.Dishes;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.DishesProfiles
{
    public class DishCreateMappingsProfile : Profile
    {
        public DishCreateMappingsProfile()
        {
            CreateMap<Dishes, DishCreateDto>();
            CreateMap<DishCreateDto, Dishes>()
                .ForMember(p => p.Id, map => map.Ignore())
                .ForMember(p => p.ModifiedOn, map => map.Ignore());
        }
    }
}