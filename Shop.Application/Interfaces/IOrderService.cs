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
	/// <summary>
	/// Order service interface.
	/// </summary>
	public interface IOrderService
	{
		/// <summary>
		/// Adds order to current user cart by cart Id.
		/// </summary>
		/// <param name="cartId"> cart Id. </param>
		/// <returns></returns>
		public Task AddOrder(Guid cartId);

		/// <summary>
		/// Gets orders by condition.
		/// </summary>
		/// <param name="predicate"> Condition. </param>
		/// <returns> Orders that fits the condition. </returns>
		public Task<IEnumerable<Order>> GetByCondition(Expression<Func<Order, bool>> predicate);

		/// <summary>
		/// Checks is exist any uncompleted orders of current user.
		/// </summary>
		/// <returns> True if uncompleted orders exist. </returns>
		public Task<bool> CheckIsAnyUncompletedOrders();

		/// <summary>
		/// Gets last uncompleted order of current user.
		/// </summary>
		/// <returns> Last uncompleted order. </returns>
		public Task<Order> GetLatestUncompletedOrderOfCurrentUser();

		/// <summary>
		/// Adds new order to current user.
		/// </summary>
		/// <returns></returns>
		public Task AddOrderToCurrentUser();

		/// <summary>
		/// Gets orders with all info for current user.
		/// </summary>
		/// <returns> Orders with all info for current user. </returns>
		public Task<IEnumerable<OrderDto>> GetOrdersWithAllInfoForCurrentUser();
	}
}
