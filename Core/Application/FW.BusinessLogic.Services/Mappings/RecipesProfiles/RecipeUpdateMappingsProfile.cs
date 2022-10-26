using AutoMapper;
using FW.BusinessLogic.Contracts.Recipes;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.RecipesProfiles
{
    public class RecipeUpdateMappingsProfile : Profile
    {
        public RecipeUpdateMappingsProfile()
        {
            CreateMap<Recipes, RecipeUpdateDto>();
            CreateMap<RecipeUpdateDto, Recipes>()
                .ForMember(p => p.ModifiedOn, map => map.Ignore())
                .ForMember(p => p.Ingredients, map => map.Ignore())
                .ForMember(p => p.Dishes, map => map.Ignore());
        }
    }
}
