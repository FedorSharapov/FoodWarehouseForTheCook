namespace FW.BusinessLogic.Contracts.Products
{
    public class ProductsGetPageDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Guid UserId { get; set; }
    }
}
