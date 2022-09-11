using AutoMapper;
using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.MapperResolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Mappings
{
	public class EntityDtosProfile : Profile
	{
		public EntityDtosProfile()
		{
			CreateMap<User, UserDto>().ReverseMap();
			CreateMap<Item, ItemDto>().ReverseMap();
			CreateMap<Order, OrderDto>()
				.ForMember(dest => dest.OrderItemDtos, opt => opt.MapFrom(src => src.OrderItems))
				.ForMember(dest => dest.TotalPrice, o => o.MapFrom<OrderTotalResolver>());
			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(dest => dest.ItemDto, opt => opt.MapFrom(src => src.Item))
				.ForMember(dest => dest.ItemQuntity, opt => opt.MapFrom(src => src.Quantity))
				.ReverseMap();
			CreateMap<OrderItem, OrderItemAddingDto>().ReverseMap();
		}
	}
}
