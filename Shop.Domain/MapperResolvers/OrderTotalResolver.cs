using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.MapperResolvers
{
	public class OrderTotalResolver : AutoMapper.IValueResolver<Order, OrderDto, decimal>
	{
		public decimal Resolve(Order source, OrderDto destination, decimal destMember, AutoMapper.ResolutionContext context)
		{
			var total = 0m;

			source.OrderItems.ForEach(x => total += x.Quantity * x.Item.Price);

			return total;
		}
	}
}
