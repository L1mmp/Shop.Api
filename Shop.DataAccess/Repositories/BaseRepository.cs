using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shop.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Repositories
{
	public abstract class BaseRepository<TEntity> where TEntity : class
	{
		protected readonly ApplicationDbContext _dbContext;
		private readonly DbSet<TEntity> _dbSet;

		protected BaseRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = dbContext.Set<TEntity>();
		}

		public async Task<EntityEntry<TEntity>> Add(TEntity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("Entity must be not null");
			}

			var addedEntity = await _dbSet.AddAsync(entity);

			await _dbContext.SaveChangesAsync();

			return addedEntity;
		}

		public async Task DeleteById(Guid id)
		{
			_dbSet.Remove(await GetById(id));
		}

		public async Task<IEnumerable<TEntity>> GetAll()
		{
			return await _dbSet.AsNoTracking().ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> predicate)
		{
			return await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
		}

		public async Task<TEntity> GetById(Guid id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task Update(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
		}

		public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
		{
			return Include(includeProperties).ToList();
		}

		public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
		{
			var query = Include(includeProperties);
			return query.Where(predicate).ToList();
		}

		public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
		{
			IQueryable<TEntity> query = _dbSet.AsNoTracking();
			return includeProperties
				.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
		}

		public async Task<IEnumerable<TEntity>> GetWithIncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties)
		{
			return await (await IncludeAsync(includeProperties)).ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetWithIncludeAsync(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
		{
			var query = await IncludeAsync(includeProperties);

			return query.Where(predicate).ToList();
		}

		public async Task<IQueryable<TEntity>> IncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties)
		{
			IQueryable<TEntity> query = _dbSet.AsNoTracking();

			var task = Task.Run(() =>
			{
				return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
			});

			return await task;
		}
	}
}
