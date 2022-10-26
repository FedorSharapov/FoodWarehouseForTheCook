namespace FW.Web.ViewModels.Recipes
{
    public class RecipeResponseVM
    {
        public Guid Id { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid IngredientId { get; set; }
        public Guid DishesId { get; set; }
        public double Quantity { get; set; }
    }
}
