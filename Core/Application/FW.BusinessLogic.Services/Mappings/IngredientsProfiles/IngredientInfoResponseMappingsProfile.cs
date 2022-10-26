using AutoMapper;
using FW.BusinessLogic.Contracts.Ingredients;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.IngredientsProfiles
{
    public class IngredientInfoResponseMappingsProfile : Profile
    {
        public IngredientInfoResponseMappingsProfile()
        {
            CreateMap<Ingredients, IngredientResponseDto>();
            CreateMap<IngredientResponseDto, Ingredients>()
                 .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}