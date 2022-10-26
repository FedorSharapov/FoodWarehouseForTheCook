using AutoMapper;
using FW.BusinessLogic.Contracts.Category;
using FW.Web.ViewModels.Categories;

namespace FW.Web.Configurations.Mappings.CategoryProfiles
{
    public class CategoryInfoQueryMappingsProfile : Profile
    {
        public CategoryInfoQueryMappingsProfile()
        {
            CreateMap<CategoryResponseVM, CategoryResponseDto>();
            CreateMap<CategoryResponseDto, CategoryResponseVM>();
        }
    }
}
