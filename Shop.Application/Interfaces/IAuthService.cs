using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Dtos;
using Shop.Domain.Models;
using Shop.Domain.ResponceModels;

namespace Shop.Application.Interfaces
{
	public interface IAuthService
	{
		public Task<RefreshToken> Register(UserDto dto);
		public Task<LoginResponceModel> Login(LoginDto dto);
		public Task<RefreshResponceModel> RefreshToken();
		public Task<RefreshToken> GetUserRefreshToken();
	}
}
