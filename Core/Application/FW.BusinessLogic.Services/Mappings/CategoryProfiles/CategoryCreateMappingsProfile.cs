using AutoMapper;
using FW.BusinessLogic.Contracts.Category;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.CategoryProfiles
{
    public class CategoryCreateMappingsProfile : Profile
    {
        public CategoryCreateMappingsProfile()
        {
            CreateMap<Categories, CategoryCreateDto>();
            CreateMap<CategoryCreateDto, Categories>()
                .ForMember(p => p.Id, map => map.Ignore())
                .ForMember(p => p.ModifiedOn, map => map.Ignore());
        }
    }
}