using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Repositories.Interfaces
{
	public interface IOrderRepository : IBaseRepository<Order>
	{
		public Task<IEnumerable<Order>> GetOrdersWithAllInfoForCurrentUser(Guid cartId);
	}
}
