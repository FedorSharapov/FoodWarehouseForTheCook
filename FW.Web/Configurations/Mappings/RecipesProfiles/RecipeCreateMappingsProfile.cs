using AutoMapper;
using FW.BusinessLogic.Contracts.Recipes;
using FW.Web.ViewModels.Recipes;

namespace FW.Web.Configurations.Mappings.RecipesProfiles
{
    public class RecipeCreateMappingsProfile : Profile
    {
        public RecipeCreateMappingsProfile()
        {
            CreateMap<RecipeCreateDto, RecipeVM>();
            CreateMap<RecipeVM, RecipeCreateDto>()
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}
