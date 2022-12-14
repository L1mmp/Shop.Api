using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Domain.Dtos;
using Shop.Domain.Models;
using Shop.Domain.ResponceModels;

namespace Shop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly IHttpContextAccessor _httpContext;
		public AuthController(IAuthService authService, IHttpContextAccessor httpContext)
		{
			_authService = authService;
			_httpContext = httpContext;
		}

		/// <summary>
		/// Register new user.
		/// </summary>
		/// <param name="userDto"> User data. </param>
		/// <returns> 200 if registration was successful. 400 if registration wasn't successful. </returns>
		[HttpPost("register")]
		public async Task<ActionResult> Register(UserDto userDto)
		{
			var refreshToken = await _authService.Register(userDto);
			SetRefreshToken(refreshToken);

			return Ok("Registration successful!");
		}

		/// <summary>
		/// Tries to login user.
		/// </summary>
		/// <param name="loginDto"> UserDTO. </param>
		/// <returns> Login responce model. </returns>
		[HttpPost("login")]
		public async Task<ActionResult<LoginResponceModel>> Login([FromBody] LoginDto loginDto)
		{
			var responce = await _authService.Login(loginDto);
			SetRefreshToken(responce.RefreshToken);

			return Ok(responce);
		}

		/// <summary>
		/// Generate and set Refresh token.
		/// </summary>
		/// <returns> Refresh token responce model. </returns>
		[HttpPost("refresh-token"), Authorize]
		public async Task<ActionResult<string>> RefreshToken()
		{
			var responce = await _authService.RefreshToken();

			SetRefreshToken(responce.RefreshToken);

			return Ok(responce);
		}

		/// <summary>
		/// Set refresh token to cookie.
		/// </summary>
		/// <param name="token"> Token to set. </param>
		private void SetRefreshToken(RefreshToken token)
		{
			var cookie = new CookieOptions()
			{
				HttpOnly = true,
				Expires = token.Expires,
			};

			Response.Cookies.Append("refreshToken", token.Token, cookie);
		}
	}
}
