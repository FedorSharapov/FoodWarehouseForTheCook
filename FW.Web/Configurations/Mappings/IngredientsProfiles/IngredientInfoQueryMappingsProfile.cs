using AutoMapper;
using FW.BusinessLogic.Contracts.Ingredients;
using FW.Web.ViewModels.Ingredients;

namespace FW.Web.Configurations.Mappings.IngredientsProfiles
{
    public class IngredientInfoQueryMappingsProfile : Profile
    {
        public IngredientInfoQueryMappingsProfile()
        {
            CreateMap<IngredientResponseVM, IngredientResponseDto>();
            CreateMap<IngredientResponseDto, IngredientResponseVM>();
        }
    }
}
