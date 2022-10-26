using AutoMapper;
using FW.BusinessLogic.Contracts.Warehouses;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.WarehousesProfiles
{
    public class WarehouseInfoResponseMappingsProfile : Profile
    {
        public WarehouseInfoResponseMappingsProfile()
        {
            CreateMap<Warehouses, WarehouseResponseDto >();
            CreateMap<WarehouseResponseDto , Warehouses>()
                .ForMember(p => p.Id, map => map.Ignore());
        }
    }
}