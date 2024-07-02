using OrdersManagement.Domain.Entities;
using OrdersManagement.Infrastructure.Data;
using OrdersManagement.Infrastructure.Interfaces;

namespace OrdersManagement.Infrastructure.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly AppDbContext _context;

        public BillingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveBillingEntriesAsync(IEnumerable<BillingEntry> billingEntries)
        {
            try
            {
                foreach(var entry in billingEntries)
                {
                    var order = _context.Orders.FirstOrDefault(x => x.Id == entry.OrderId);
                    var offer = _context.Offers.FirstOrDefault(x => x.Id == entry.OfferId);

                    _context.BillingEntries.Add(new BillingEntry()
                    {
                        BillingEntryId = entry.BillingEntryId,
                        OccurredAt = entry.OccurredAt,
                        BillingTypeId = entry.BillingTypeId,
                        BillingTypeName = entry.BillingTypeName,
                        Offer = offer,
                        ValueAmount = entry.ValueAmount,
                        TaxPercentage = entry.TaxPercentage,
                        TaxAnnotation = entry.TaxAnnotation,
                        BalanceAmount = entry.BalanceAmount,
                        BalanceCurrency = entry.BalanceCurrency,
                        Order = order
                    });

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while trying to save new billing entry to database. Message: {ex.Message}");
            }
        }

        public bool BillingEntryExists(string id) =>
             _context.BillingEntries.Any(x => x.BillingEntryId == id);
    }
}
