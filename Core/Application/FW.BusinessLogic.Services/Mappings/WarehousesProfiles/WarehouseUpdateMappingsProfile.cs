using AutoMapper;
using FW.BusinessLogic.Contracts.Warehouses;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.WarehousesProfiles
{
    public class WarehouseUpdateMappingsProfile : Profile
    {
        public WarehouseUpdateMappingsProfile()
        {
            CreateMap<Warehouses, WarehouseUpdateDto>();
            CreateMap<WarehouseUpdateDto, Warehouses>()
                .ForMember(p => p.Id, map => map.Ignore())
                .ForMember(p => p.ModifiedOn, map => map.Ignore());
        }
    }
}
