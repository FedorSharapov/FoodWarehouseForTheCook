using AutoMapper;
using FW.BusinessLogic.Contracts.Category;
using FW.Web.ViewModels.Categories;

namespace FW.Web.Configurations.Mappings.CategoryProfiles
{
    public class CategoryCreateMappingsProfile : Profile
    {
        public CategoryCreateMappingsProfile()
        {
            CreateMap<CategoryCreateDto, CategoryVM>();
            CreateMap<CategoryVM, CategoryCreateDto>()
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}
