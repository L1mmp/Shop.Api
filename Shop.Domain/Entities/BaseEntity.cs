using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
	public abstract class BaseEntity
	{
		[Key]
		[Column(Order = 0)]
		public virtual Guid Id { get; set; }
	}
}
