using AutoMapper;
using FW.BusinessLogic.Contracts.Category;
using FW.Web.ViewModels.Categories;

namespace FW.Web.Configurations.Mappings.CategoryProfiles
{
    public class CategoryUpdateMappingsProfile : Profile
    {
        public CategoryUpdateMappingsProfile()
        {
            CreateMap<CategoryUpdateDto, CategoryVM>();
            CreateMap<CategoryVM, CategoryUpdateDto>()
                .ForMember(p => p.Id, map => map.Ignore())
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}
