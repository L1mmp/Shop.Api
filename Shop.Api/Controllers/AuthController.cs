using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Domain.Dtos;
using Shop.Domain.ResponceModels;

namespace Shop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUserService _userService;

		public AuthController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("register")]
		public async Task<ActionResult> Register(UserDto userDto)
		{
			if (userDto == null)
			{
				return BadRequest("User is null");
			}
			
			await _userService.AddUser(userDto);


			return Ok("Registration successfull!");
		}

		[HttpPost("login")]
		
		public async Task<ActionResult<LoginResponceModel>> Login([FromBody] LoginDto loginDto)
		{
			return Ok(await _userService.TryLogin(loginDto));
		}
	}
}
