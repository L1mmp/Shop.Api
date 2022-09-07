using AutoMapper;
using Shop.Domain.Dtos;
using Shop.Domain.Entities;
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
			CreateMap<Order, OrderDto>().ReverseMap();
		}
	}
}
