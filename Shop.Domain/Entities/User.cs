using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;

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
		public string RefreshToken { get; set; } = string.Empty;
		public DateTime TokenCreated { get; set; }
		public DateTime TokenExpires { get; set; }
	}
}
