using AutoMapper;
using FW.BusinessLogic.Contracts.Ingredients;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.IngredientsProfiles
{
    public class IngredientUpdateMappingsProfile : Profile
    {
        public IngredientUpdateMappingsProfile()
        {
            CreateMap<Ingredients, IngredientUpdateDto>();
            CreateMap<IngredientUpdateDto, Ingredients>()
                .ForMember(p => p.ModifiedOn, map => map.Ignore());
        }
    }
}
