using AutoMapper;
using FW.BusinessLogic.Contracts.Recipes;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.RecipesProfiles
{
    public class RecipeInfoResponseMappingsProfile : Profile
    {
        public RecipeInfoResponseMappingsProfile()
        {
            CreateMap<Recipes, RecipeResponseDto >();
            CreateMap<RecipeResponseDto , Recipes>()
                .ForMember(p => p.Ingredients, map => map.Ignore())
                .ForMember(p => p.Dishes, map => map.Ignore())
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}