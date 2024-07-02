using OrdersManagement.Application.Models;
using OrdersManagement.Application.Services;

namespace OrdersManagement.Application.Interfaces
{
    public interface IBillingService
    {
        Task AddBillingEntriesByOrderIdAsync();

        Task AddBillingEntriesByOfferIdAsync();
    }
}
