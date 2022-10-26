using AutoMapper;
using FW.BusinessLogic.Contracts.Ingredients;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.IngredientsProfiles
{
    public class IngredientCreateMappingsProfile : Profile
    {
        public IngredientCreateMappingsProfile()
        {
            CreateMap<Ingredients, IngredientCreateDto>();
            CreateMap<IngredientCreateDto, Ingredients>()
                .ForMember(p => p.Id, map => map.Ignore())
                .ForMember(p => p.ModifiedOn, map => map.Ignore());
        }
    }
}