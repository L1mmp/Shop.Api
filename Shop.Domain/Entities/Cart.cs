using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
	public class Cart : BaseEntity
	{
		public Guid UserId { get; set; }
		public User? User { get; set; }
		public List<Order>? Orders { get; set; }
	}
}
