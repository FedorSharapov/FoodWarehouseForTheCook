using AutoMapper;
using FW.BusinessLogic.Contracts.Category;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.CategoryProfiles
{
    public class CategoryUpdateMappingsProfile : Profile
    {
        public CategoryUpdateMappingsProfile()
        {
            CreateMap<Categories, CategoryUpdateDto>();
            CreateMap<CategoryUpdateDto, Categories>()
                .ForMember(p => p.ModifiedOn, map => map.Ignore());
        }
    }
}
