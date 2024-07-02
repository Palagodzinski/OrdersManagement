using Microsoft.EntityFrameworkCore;
using OrdersManagement.Infrastructure.Data;
using OrdersManagement.Infrastructure.Interfaces;

namespace OrdersManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IDictionary<int, string>> GetOrdersIdsAsync() =>
            await _context.Orders.ToDictionaryAsync(x => x.Id, x => x.OrderId);
    }
}
