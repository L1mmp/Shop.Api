using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces
{
	public interface IItemService
	{
		public Task<IEnumerable<ItemDto>> GetByCondtiton(Expression<Func<Item, bool>> expression);
		public Task AddItem(ItemDto itemDto);
		public Task UpdateItem(ItemDto itemDto, Guid itemId);
		public Task DeleteItemById(Guid itemId);
	}
}
