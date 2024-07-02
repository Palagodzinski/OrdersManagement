using Microsoft.EntityFrameworkCore;
using OrdersManagement.Infrastructure.Data;
using OrdersManagement.Infrastructure.Interfaces;


namespace OrdersManagement.Infrastructure.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AppDbContext _context;

        public OfferRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IDictionary<int, string>> GetOfferIdsAsync() =>
            await _context.Offers.ToDictionaryAsync(x => x.Id, x => x.OfferId);
    }
}
