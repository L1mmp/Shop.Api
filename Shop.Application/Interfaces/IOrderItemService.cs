using Shop.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces
{
	public interface IOrderItemService
	{
		public Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderId(Guid orderId);

	}
}
