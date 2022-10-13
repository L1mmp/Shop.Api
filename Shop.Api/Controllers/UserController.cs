using Microsoft.AspNetCore.Authorization;
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

		/// <summary>
		/// Get all users in system.
		/// </summary>
		/// <returns> All users in system. </returns>
		[HttpGet("getAll"), Authorize(Roles = "Admin")]
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
