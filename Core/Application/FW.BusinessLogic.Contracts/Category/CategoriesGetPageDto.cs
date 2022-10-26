namespace FW.BusinessLogic.Contracts.Category
{
    public class CategoriesGetPageDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Guid UserId { get; set; }
    }
}
