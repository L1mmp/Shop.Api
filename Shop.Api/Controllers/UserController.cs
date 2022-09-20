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

		[HttpGet("getAll")]
		public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
		{
			try
			{
				var users = await _userService.GetAll();
				return Ok(users);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

	}
}
