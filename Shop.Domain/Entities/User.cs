using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
	public class User : BaseEntity
	{
		public Cart? Cart { get; set; }
		public string? Name { get; set; }
		[Required]
		public int Age { get; set; }
		public string? Login { get; set; }
		[Required]
		public string? Password { get; set; }
	}
}
