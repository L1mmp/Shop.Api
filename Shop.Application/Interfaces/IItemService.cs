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
	/// <summary>
	/// Item service interface.
	/// </summary>
	public interface IItemService
	{
		/// <summary>
		/// Gets items by condition
		/// </summary>
		/// <param name="expression"> Condition. </param>
		/// <returns> Items that fits the condition. </returns>
		public Task<IEnumerable<ItemDto>> GetByCondtiton(Expression<Func<Item, bool>> expression);

		/// <summary>
		/// Adds new item.
		/// </summary>
		/// <param name="itemDto"> ItemDto. </param>
		/// <returns></returns>
		public Task AddItem(ItemDto itemDto);

		/// <summary>
		/// Updates an item.
		/// </summary>
		/// <param name="itemDto"> ItemDto </param>
		/// <param name="itemId"> item Id </param>
		/// <returns></returns>
		public Task UpdateItem(ItemDto itemDto, Guid itemId);

		/// <summary>
		/// Deletes item by id.
		/// </summary>
		/// <param name="itemId"> item Id. </param>
		/// <returns></returns>
		public Task DeleteItemById(Guid itemId);
	}
}
