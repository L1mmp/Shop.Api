using Shop.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces
{
	/// <summary>
	/// OrderItem service interface.
	/// </summary>
	public interface IOrderItemService
	{
		/// <summary>
		/// Gets order items by order Id.
		/// </summary>
		/// <param name="orderId"> order Id. </param>
		/// <returns> Order items by order Id. </returns>
		public Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderId(Guid orderId);

		/// <summary>
		/// Adds order items to order.
		/// </summary>
		/// <param name="dto"> Order item adding Dto. </param>
		/// <returns></returns>
		public Task AddOrderItemToOrder(OrderItemAddingDto dto);

		/// <summary>
		/// Deletes order items from order.
		/// </summary>
		/// <returns></returns>
		public Task DeleteOrderItemFromOrder();
	}
}
