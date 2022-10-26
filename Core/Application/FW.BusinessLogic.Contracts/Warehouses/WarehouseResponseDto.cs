namespace FW.BusinessLogic.Contracts.Warehouses
{
    public class WarehouseResponseDto 
    {
        public DateTime ModifiedOn { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
    }
}
