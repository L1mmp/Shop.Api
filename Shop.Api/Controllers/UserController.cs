using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Domain.Dtos;
using Shop.Domain.ResponceModels;

namespace Shop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("add")]
		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> AddUser([FromBody] UserDto userDto)
		{
			if (userDto == null)
			{
				return BadRequest("User is null");
			}
			try
			{
				await _userService.AddUser(userDto);
			}
			catch (Exception ex)
			{
				return BadRequest(String.Concat(ex.Message, "\n", ex.InnerException, "\n", ex.StackTrace));
			}

			return Created(nameof(GetByLogin), userDto);

		}

		[HttpGet("getAll")]
		public async Task<ActionResult> GetAllUsers()
		{
			try
			{
				await _userService.GetAll();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok();
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginResponceModel>> TryLogin([FromBody] LoginDto loginDto)
		{
			var responce = await _userService.TryLogin(loginDto);

			if (responce.IsSuccessful)
			{
				return Ok(responce);
			}

			return BadRequest(responce);

		}

		[HttpGet("getBylogin")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> GetByLogin(string login)
		{
			var user = await _userService.GetByCondtiton(u => u.Login == login);
			if (!user.Any())
			{
				return NotFound();
			}

			return Ok(user);
		}

	}
}
