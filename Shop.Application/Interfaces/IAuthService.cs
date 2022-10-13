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
	/// <summary>
	/// Auth service interface.
	/// </summary>
	public interface IAuthService
	{
		/// <summary>
		/// Register new user.
		/// </summary>
		/// <param name="dto"> UserDto. </param>
		/// <returns> Refresh token. </returns>
		public Task<RefreshToken> Register(UserDto dto);

		/// <summary>
		/// Tries to login user.
		/// </summary>
		/// <param name="dto"> LoginDto. </param>
		/// <returns> LoginResponceModel. </returns>
		public Task<LoginResponceModel> Login(LoginDto dto);

		/// <summary>
		/// Gets refresh token.
		/// </summary>
		/// <returns> RefreshResponceModel. </returns>
		public Task<RefreshResponceModel> RefreshToken();

		/// <summary>
		/// Gets user refresh token.
		/// </summary>
		/// <returns> User refresh token. </returns>
		public Task<RefreshToken> GetUserRefreshToken();
	}
}
