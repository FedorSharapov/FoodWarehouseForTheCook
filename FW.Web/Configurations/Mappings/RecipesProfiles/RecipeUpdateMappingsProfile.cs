using AutoMapper;
using FW.BusinessLogic.Contracts.Recipes;
using FW.Web.ViewModels.Recipes;

namespace FW.Web.Configurations.Mappings.RecipesProfiles
{
    public class RecipeUpdateMappingsProfile : Profile
    {
        public RecipeUpdateMappingsProfile()
        {
            CreateMap<RecipeUpdateDto, RecipeVM>();
            CreateMap<RecipeVM, RecipeUpdateDto>()
                .ForMember(p => p.Id, map => map.Ignore())
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}
