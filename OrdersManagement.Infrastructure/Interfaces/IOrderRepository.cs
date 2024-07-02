
namespace OrdersManagement.Infrastructure.Interfaces
{
    public interface IOrderRepository
    {
        Task<IDictionary<int, string>> GetOrdersIdsAsync();
    }
}
