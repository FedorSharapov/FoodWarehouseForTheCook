using AutoMapper;
using FW.BusinessLogic.Contracts.Ingredients;
using FW.Web.ViewModels.Ingredients;

namespace FW.Web.Configurations.Mappings.IngredientsProfiles
{
    public class IngredientUpdateMappingsProfile : Profile
    {
        public IngredientUpdateMappingsProfile()
        {
            CreateMap<IngredientUpdateDto, IngredientVM>();
            CreateMap<IngredientVM, IngredientUpdateDto>()
                 .ForMember(p => p.Id, map => map.Ignore())
                 .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}
