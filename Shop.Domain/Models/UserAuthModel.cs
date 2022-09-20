using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.Domain.Models
{
	public class UserAuthModel
	{
		public string Login { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public int Age { get; set; }
		public string RefreshToken { get; set; } = string.Empty;
		public DateTime TokenCreated { get; set; }
		public DateTime TokenExpires { get; set; }
	}
}
