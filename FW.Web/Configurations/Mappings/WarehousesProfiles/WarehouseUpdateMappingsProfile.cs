using AutoMapper;
using FW.BusinessLogic.Contracts.Warehouses;
using FW.Web.ViewModels.Warehouses;

namespace FW.Web.Configurations.Mappings.WarehousesProfiles
{
    public class WarehouseUpdateMappingsProfile : Profile
    {
        public WarehouseUpdateMappingsProfile()
        {
            CreateMap<WarehouseUpdateDto, WarehouseVM>();
            CreateMap<WarehouseVM, WarehouseUpdateDto>()
                .ForMember(p => p.UserId, map => map.Ignore());
        }
    }
}
