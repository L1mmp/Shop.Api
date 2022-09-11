using AutoMapper;
using Microsoft.AspNetCore.Http;
using Shop.Application.Interfaces;
using Shop.DataAccess.Repositories;
using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
	public class OrderItemService : IOrderItemService
	{
		IBaseRepository<OrderItem> _orderItemRepository;
		IMapper _mapper;
		IHttpContextAccessor _httpContext;
		IOrderService _orderService;
		IItemService _itemService;
		public OrderItemService(IBaseRepository<OrderItem> orderItemRepository, IMapper mapper, IHttpContextAccessor httpContext, IOrderService orderService, IItemService itemService)
		{
			_orderItemRepository = orderItemRepository;
			_mapper = mapper;
			_httpContext = httpContext;
			_orderService = orderService;
			_itemService = itemService;
		}

		public async Task AddOrderItemToOrder(OrderItemAddingDto dto)
		{
			var IsAnyUncompletedOrders = await _orderService.CheckIsAnyUncompletedOrders();
			var currentCartId = new Guid(_httpContext.HttpContext.User.FindFirst("cartId").Value);

			if (!IsAnyUncompletedOrders)
			{
				await _orderService.AddOrder(currentCartId);
			}

			var lastOrderId = (await _orderService.GetLatestUncompletedOrderOfCurrentUser()).Id;

			var orderItem = _mapper.Map<OrderItem>(dto);
			orderItem.OrderId = lastOrderId;

			await _orderItemRepository.Add(orderItem);
		}

		public Task DeleteOrderItemFromOrder()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderId(Guid orderId)
		{
			throw new NotImplementedException();
		}


	}
}
