using AutoMapper;
using FW.BusinessLogic.Contracts.Category;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.CategoryProfiles
{
    public class CategoryInfoResponseMappingsProfile : Profile
    {
        public CategoryInfoResponseMappingsProfile()
        {
            CreateMap<Categories, CategoryResponseDto>();
            CreateMap<CategoryResponseDto, Categories>()
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}