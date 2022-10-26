using AutoMapper;
using FW.BusinessLogic.Contracts.Dishes;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.DishesProfiles
{
    public class DishInfoResponseMappingsProfile : Profile
    {
        public DishInfoResponseMappingsProfile()
        {
            CreateMap<Dishes, DishResponseDto>();
            CreateMap<DishResponseDto, Dishes>()
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}