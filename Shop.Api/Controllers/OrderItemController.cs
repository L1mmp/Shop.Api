using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Domain.Dtos;

namespace Shop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderItemController : ControllerBase
	{
		IOrderItemService _orderItemService;

		public OrderItemController(IOrderItemService orderItemService)
		{
			_orderItemService = orderItemService;
		}

		[HttpPost("addOrderItemToOrder"), Authorize]
		public async Task<ActionResult> AddOrderItemToOrder(OrderItemAddingDto dto)
		{
			await _orderItemService.AddOrderItemToOrder(dto);
			return Ok();
		}
	}
}
