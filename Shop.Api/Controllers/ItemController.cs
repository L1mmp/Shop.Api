using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Dtos;

namespace Shop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ItemController : ControllerBase
	{
		private readonly IItemService _itemService;

		public ItemController(IItemService itemService)
		{
			_itemService = itemService;
		}

		/// <summary>
		/// Get item info by Id.
		/// </summary>
		/// <param name="id"> Id. </param>
		/// <returns> Item info. </returns>
		[HttpGet("getById"), Authorize]
		public async Task<ActionResult> GetItemById(Guid id)
		{
			var item = await _itemService.GetByCondtiton(x => x.Id == id);
			if (item.Any())
			{
				return Ok(item);
			}
			return NotFound("Item not found");

		}

		/// <summary>
		/// Adds item.
		/// </summary>
		/// <param name="itemDto"> itemDto. </param>
		/// <returns> Ok if add was successful. </returns>
		[HttpPost("add"), Authorize(Roles = "Admin")]
		public async Task<ActionResult> AddItem([FromBody]ItemDto itemDto)
		{
			if (itemDto == null)
				return BadRequest("Item is null");


			await _itemService.AddItem(itemDto);
			return Ok(itemDto);
		}

		/// <summary>
		/// Updates item by Id.
		/// </summary>
		/// <param name="itemDto"> itemDto. </param>
		/// <param name="itemId"> Item Id. </param>
		/// <returns> Ok if update was successful </returns>
		[HttpPut("update")]
		public async Task<ActionResult> UpdateItem([FromBody] ItemDto itemDto, Guid itemId)
		{
			if (itemDto == null)
				return BadRequest("Item is null");

			await _itemService.UpdateItem(itemDto, itemId);
			return Ok(itemDto);
		}
	}
}
