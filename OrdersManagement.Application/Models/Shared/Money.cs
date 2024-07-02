namespace OrdersManagement.Application.Models.Shared
{
    public abstract record Money
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
