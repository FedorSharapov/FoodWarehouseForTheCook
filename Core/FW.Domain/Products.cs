using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FW.Domain
{
    public class Products : Base
    {
        [Required]
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }

        public Guid UserId { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid IngredientId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouses Warehouse { get; set; }
        [ForeignKey("CategoryId")]
        public Categories Category { get; set; }
        [ForeignKey("IngredientId")]
        public Ingredients Ingredients { get; set; }
    }
}
