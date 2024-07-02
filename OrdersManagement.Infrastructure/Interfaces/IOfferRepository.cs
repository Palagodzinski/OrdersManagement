
namespace OrdersManagement.Infrastructure.Interfaces
{
    public interface IOfferRepository
    {
        Task<IDictionary<int, string>> GetOfferIdsAsync();
    }
}
