namespace FW.BusinessLogic.Contracts.Products
{
    public class ProductUpdateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Quantity { get; set; }
    }
}
