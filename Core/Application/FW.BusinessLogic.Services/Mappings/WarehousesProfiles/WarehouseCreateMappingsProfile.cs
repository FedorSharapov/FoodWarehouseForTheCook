using AutoMapper;
using FW.BusinessLogic.Contracts.Warehouses;
using FW.Domain;

namespace FW.BusinessLogic.Services.Mappings.WarehousesProfiles
{
    public class WarehouseCreateMappingsProfile : Profile
    {
        public WarehouseCreateMappingsProfile()
        {
            CreateMap<Warehouses, WarehouseCreateDto>();
            CreateMap<WarehouseCreateDto, Warehouses>()
                .ForMember(p => p.Id, map => map.Ignore())
                .ForMember(p => p.ModifiedOn, map => map.Ignore());
        }
    }
}