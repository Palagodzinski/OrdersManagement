namespace OrdersManagement.Application.Models.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int? ErpOrderId { get; set; }
        public int? InvoiceId { get; set; }
        public int? StoreId { get; set; }
    }
}