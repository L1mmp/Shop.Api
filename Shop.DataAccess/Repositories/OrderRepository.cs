using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Entities;

namespace Shop.DataAccess.Repositories
{
	public class OrderRepository : BaseRepository<Order>, IOrderRepository
	{
		public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<IEnumerable<Order>> GetOrdersWithAllInfoForCurrentUser(Guid cartId)
		{
			return await Task.Factory.StartNew(
				() =>
				{
					var orders = (from order in _dbContext.Orders
								  where order.CartId == cartId
								  select order).ToList();
					var orderIds = new List<Guid>();
					orders.ForEach((x) =>
					{
						orderIds.Add(x.Id);
					});

					var orderItemEntities = _dbContext.OrderItems.Where(x => orderIds.Contains(x.OrderId)).Join(_dbContext.Items, o => o.ItemId, i => i.Id, (o, i)
						=> new OrderItem { Item = i, OrderId = o.OrderId, Quantity = o.Quantity });
					//var orderItemEntities = from oItem in _dbContext.OrderItems
					//						join itemInfo in _dbContext.Items on oItem.Id equals itemInfo.Id
					//						where oItem.Id 
					//						select new OrderItem
					//						{
					//							Item = itemInfo,
					//							Order = order,
					//							Quantity = oItem.Quantity
					//						};


					foreach (var order in orders)
					{
						order.OrderItems = orderItemEntities.Where(x => x.OrderId == order.Id).ToList();
					}

					return orders;
				}
				);
		}
	}
}
