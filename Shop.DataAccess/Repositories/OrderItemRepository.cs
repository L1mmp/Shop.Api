using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Entities;

namespace Shop.DataAccess.Repositories
{
	public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
	{
		public OrderItemRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
