using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Entities;

namespace Shop.DataAccess.Repositories
{
	public class OrderItemRepository : BaseRepository<OrderItem>, IBaseRepository<OrderItem>
	{
		public OrderItemRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
