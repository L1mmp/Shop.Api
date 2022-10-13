using AutoMapper;
using Shop.Application.Interfaces;
using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Services
{
	public class ItemService : IItemService
	{
		private readonly IItemRepository _itemRepositroy;
		private readonly IMapper _mapper;

		public ItemService(IItemRepository itemRepositroy, IMapper mapper)
		{
			_itemRepositroy = itemRepositroy;
			_mapper = mapper;
		}

		public async Task AddItem(ItemDto itemDto)
		{
			var entity = _mapper.Map<Item>(itemDto);
			await _itemRepositroy.AddAsync(entity);
		}

		public async Task DeleteItemById(Guid itemId)
		{
			await _itemRepositroy.DeleteByIdAsync(itemId);
		}

		public async Task<IEnumerable<ItemDto>> GetByCondtiton(Expression<Func<Item, bool>> expression)
		{
			var items = await _itemRepositroy.GetByConditionAsync(expression);
			return _mapper.Map<IEnumerable<ItemDto>>(items);
		}

		public async Task UpdateItem(ItemDto itemDto, Guid itemId)
		{
			var entity = _mapper.Map<Item>(itemDto);
			entity.Id = itemId;
			await _itemRepositroy.UpdateAsync(entity);
		}
	}
}
