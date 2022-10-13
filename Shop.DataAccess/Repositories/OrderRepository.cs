using Microsoft.EntityFrameworkCore;
using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Entities;

namespace Shop.DataAccess.Repositories
{
	public class OrderRepository : BaseRepository<Order>, IOrderRepository
	{
		public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<IEnumerable<Order>> GetOrdersWithAllInfoForCurrentUser(Guid cartId)
		{
			return await Task.Factory.StartNew(
				() => _dbContext.Orders.AsNoTracking().Where(x => x.CartId == cartId)
					.Include(x => x.OrderItems)!
					.ThenInclude(t => t.Item));
		}
	}
}
