namespace FW.BusinessLogic.Contracts.Dishes
{
    public class DishResponseDto
    {
        public Guid Id { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
