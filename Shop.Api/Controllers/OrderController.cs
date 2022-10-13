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

		/// <summary>
		/// Creates new order.
		/// </summary>
		/// <returns> Ok if add was successful. </returns>
		[HttpPost("addOrder"), Authorize]
		public async Task<ActionResult> AddOrder()
		{
			await _orderService.AddOrderToCurrentUser();
			return Ok();
		}

		/// <summary>
		/// Gets all user orders with items info.
		/// </summary>
		/// <returns> All user orders with items info. </returns>
		[HttpGet("getOrders"), Authorize]
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersWithAllInfoForCurrentUser()
		{
			return Ok(await _orderService.GetOrdersWithAllInfoForCurrentUser());
		}
	}
}
