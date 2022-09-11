using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dtos
{
	public class OrderItemAddingDto
	{
		public Guid itemId { get; set; }
		public int Quantity { get; set; }
	}
}
