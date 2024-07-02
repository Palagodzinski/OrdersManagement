
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Infrastructure.Interfaces
{
    public interface IBillingRepository
    {
        Task SaveBillingEntriesAsync(IEnumerable<BillingEntry> billingEntries);

        bool BillingEntryExists(string id);
    }
}
