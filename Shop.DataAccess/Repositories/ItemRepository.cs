using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Entities;

namespace Shop.DataAccess.Repositories
{
	public class ItemRepository : BaseRepository<Item>, IBaseRepository<Item>
	{
		public ItemRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
