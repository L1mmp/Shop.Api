using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces
{
	public interface IOrderService
	{
		public Task<IEnumerable<OrderDto>> GetAllUserOrdersWithInfo();
		public Task AddOrder(OrderDto orderDto);

	}
}
