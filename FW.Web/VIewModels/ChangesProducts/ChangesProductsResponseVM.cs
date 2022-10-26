namespace FW.Web.ViewModels.ChangesProducts
{
    public class ChangesProductResponseVM
    {
        public Guid Id { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
