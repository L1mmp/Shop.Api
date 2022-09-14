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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContext;
		private readonly IBaseRepository<Item> _itemRepository;
		public OrderService(IOrderRepository orderRepository, IMapper mapper, IHttpContextAccessor httpContext, IBaseRepository<Item> itemRepository)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
			_httpContext = httpContext;
			_itemRepository = itemRepository;
		}

		public async Task<IEnumerable<OrderDto>> GetAllUserOrdersWithInfo()
		{
			var currentCartId = new Guid(_httpContext.HttpContext.User.FindFirst("cartId").Value);
			var orders = await _orderRepository.GetWithIncludeAsync(x => x.CartId == currentCartId, o => o.OrderItems);

			var itemsIds = orders.SelectMany(x => x.OrderItems.Select(x => x.ItemId).Distinct());

			var itemsInfo = (await _itemRepository.GetByCondition((x) => itemsIds.Contains(x.Id)));

			orders.ToList()
			.ForEach(x => x.OrderItems
			.ForEach(x => x.Item = itemsInfo.Where(t => t.Id == x.ItemId).FirstOrDefault()));

			var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
			return ordersDto;
		}

		public async Task<IEnumerable<OrderDto>> GetOrdersWithAllInfoForCurrentUser()
		{
			var currentCartId = new Guid(_httpContext.HttpContext.User.FindFirst("cartId").Value);

			var orders = await _orderRepository.GetOrdersWithAllInfoForCurrentUser(currentCartId);
			return _mapper.Map<IEnumerable<OrderDto>>(orders);
		}

		public async Task AddOrder(Guid cartId)
		{
			var entity = new Order()
			{
				CartId = cartId,
				Status = "Uncompleted"
			};
			await _orderRepository.Add(entity);
		}

		public async Task AddOrderToCurrentUser()
		{
			var currentCartId = new Guid(_httpContext.HttpContext.User.FindFirst("cartId").Value);
			var entity = new Order()
			{
				CartId = currentCartId,
				Status = "Uncompleted",
				OrderDate = DateTime.UtcNow,
			};
			await _orderRepository.Add(entity);
		}

		public async Task<bool> CheckIsAnyUncompletedOrders()
		{
			var currentCartId = new Guid(_httpContext.HttpContext.User.FindFirst("cartId").Value);

			return (await _orderRepository.GetByCondition(x => (x.CartId == currentCartId) && (x.Status == "Uncompleted"))).Any();
		}

		public async Task<IEnumerable<Order>> GetByCondition(Expression<Func<Order, bool>> predicate)
		{
			return await _orderRepository.GetByCondition(predicate);
		}

		public async Task<Order> GetLatestUncompletedOrderOfCurrentUser()
		{
			var currentCartId = new Guid(_httpContext.HttpContext.User.FindFirst("cartId").Value);

			return (await _orderRepository.GetByCondition(x => x.CartId == currentCartId && x.Status == "Uncompleted")).OrderBy(x => x.OrderDate).FirstOrDefault();
		}
	}
}
