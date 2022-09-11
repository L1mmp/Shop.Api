using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces
{
	public interface IOrderService
	{
		public Task<IEnumerable<OrderDto>> GetAllUserOrdersWithInfo();
		public Task AddOrder(Guid cartId);
		public Task<IEnumerable<Order>> GetByCondition(Expression<Func<Order, bool>> predicate);
		public Task<bool> CheckIsAnyUncompletedOrders();
		public Task<Order> GetLatestUncompletedOrderOfCurrentUser();
		public Task AddOrderToCurrentUser();
	}
}
