using AutoMapper;
using FW.BusinessLogic.Contracts.Ingredients;
using FW.Web.ViewModels.Ingredients;

namespace FW.Web.Configurations.Mappings.IngredientsProfiles
{
    public class IngredientCreateMappingsProfile : Profile
    {
        public IngredientCreateMappingsProfile()
        {
            CreateMap<IngredientCreateDto, IngredientVM>();
            CreateMap<IngredientVM, IngredientCreateDto>()
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}
