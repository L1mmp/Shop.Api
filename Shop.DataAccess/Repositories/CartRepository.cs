using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Entities;

namespace Shop.DataAccess.Repositories
{
	public class CartRepository : BaseRepository<Cart>, IBaseRepository<Cart>
	{
		public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
