namespace FW.Web.ViewModels.Products
{
    public class ProductResponseVM
    {
        public Guid Id { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Quantity { get; set; }
    }
}
