using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.DataAccess.Repositories.Interfaces
{
	public interface IOrderItemRepository : IBaseRepository<OrderItem>
	{
	}
}
