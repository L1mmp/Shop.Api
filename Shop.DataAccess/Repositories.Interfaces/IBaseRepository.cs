using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Shop.DataAccess.Repositories.Interfaces
{
	public interface IBaseRepository<TEntity> where TEntity : class
	{
		public Task<EntityEntry<TEntity>> Add(TEntity entity);
		public Task<TEntity> GetById(Guid id);
		public Task<IEnumerable<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> predicate);
		public Task<IEnumerable<TEntity>> GetAll();
		public Task DeleteById(Guid id);
		public Task Update(TEntity entity);
		public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
		public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
		public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties);
	}
}
