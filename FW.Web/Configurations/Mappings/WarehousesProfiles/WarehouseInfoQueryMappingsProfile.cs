using AutoMapper;
using FW.BusinessLogic.Contracts.Warehouses;
using FW.Web.ViewModels.Warehouses;

namespace FW.Web.Configurations.Mappings.WarehousesProfiles
{
    public class WarehouseInfoQueryMappingsProfile : Profile
    {
        public WarehouseInfoQueryMappingsProfile()
        {
            CreateMap<WarehouseResponseVM, WarehouseResponseDto >();
            CreateMap<WarehouseResponseDto , WarehouseResponseVM>();
        }
    }
}
