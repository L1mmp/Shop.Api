using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Models;

namespace Shop.Domain.ResponceModels
{
	public class LoginResponceModel
	{
		public string? Message { get; set; }
		public bool IsSuccessful { get; set; }
		public string? Token { get; set; }
		public RefreshToken RefreshToken { get; set; }
	}
}
