using AutoMapper;
using FW.BusinessLogic.Contracts.Recipes;
using FW.Web.ViewModels.Recipes;

namespace FW.Web.Configurations.Mappings.RecipesProfiles
{
    public class RecipeInfoQueryMappingsProfile : Profile
    {
        public RecipeInfoQueryMappingsProfile()
        {
            CreateMap<RecipeResponseVM, RecipeResponseDto >();
            CreateMap<RecipeResponseDto , RecipeResponseVM>();
        }
    }
}
