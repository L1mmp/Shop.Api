using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Entities;

namespace Shop.DataAccess.Repositories
{
	public class OrderRepository : BaseRepository<Order>, IBaseRepository<Order>
	{
		public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
