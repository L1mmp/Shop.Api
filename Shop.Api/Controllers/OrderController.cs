using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Interfaces;
using Shop.Domain.Dtos;

namespace Shop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpGet("getOrders"), Authorize]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllUserOrdersWithInfo()
		{
			return Ok(await _orderService.GetAllUserOrdersWithInfo());
		}

		[HttpPost("addOrder"), Authorize]
		public async Task<ActionResult> AddOrder(OrderDto orderDto)
		{
			await _orderService.AddOrder(orderDto);
			return Ok();
		}
	}
}
