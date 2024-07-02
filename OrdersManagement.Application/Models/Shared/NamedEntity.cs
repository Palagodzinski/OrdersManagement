namespace OrdersManagement.Application.Models.Shared
{
    public abstract record NamedEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
