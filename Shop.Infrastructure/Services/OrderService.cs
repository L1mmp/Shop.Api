using AutoMapper;
using Microsoft.AspNetCore.Http;
using Shop.Application.Interfaces;
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
	public class OrderService : IOrderService
	{
		private readonly IBaseRepository<Order> _orderRepository;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContext;
		public OrderService(IBaseRepository<Order> orderRepository, IMapper mapper, IHttpContextAccessor httpContext)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
			_httpContext = httpContext;
		}

		public async Task<IEnumerable<OrderDto>> GetAllUserOrdersWithInfo()
		{
			var currentCartId = new Guid(_httpContext.HttpContext.User.FindFirst("cartId").Value);

			var orders = _orderRepository.GetWithInclude(x => x.CartId == currentCartId, o => o.OrderItems);

			return _mapper.Map<IEnumerable<OrderDto>>(orders);
		}

		public async Task AddOrder(OrderDto orderDto)
		{
			await _orderRepository.Add(_mapper.Map<Order>(orderDto));
		}
	}
}
