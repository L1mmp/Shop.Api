using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Entities;

namespace Shop.DataAccess.Repositories
{
	public class UserRepository : BaseRepository<User>, IBaseRepository<User>
	{
		public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
