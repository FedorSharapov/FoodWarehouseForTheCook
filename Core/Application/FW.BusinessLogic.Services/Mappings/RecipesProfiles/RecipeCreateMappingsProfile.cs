using AutoMapper;
using FW.BusinessLogic.Contracts.Recipes;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.RecipesProfiles
{
    public class RecipeCreateMappingsProfile : Profile
    {
        public RecipeCreateMappingsProfile()
        {
            CreateMap<Recipes, RecipeCreateDto>();
            CreateMap<RecipeCreateDto, Recipes>()
                .ForMember(p => p.Id, map => map.Ignore())
                .ForMember(p => p.ModifiedOn, map => map.Ignore())
                .ForMember(p => p.Ingredients, map => map.Ignore())
                .ForMember(p => p.Dishes, map => map.Ignore());
        }
    }
}