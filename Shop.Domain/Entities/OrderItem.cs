namespace Shop.Domain.Entities
{
	public class OrderItem : BaseEntity
	{
		public Guid OrderId { get; set; }
		public Guid ItemId { get; set; }
		public Item? Item { get; set; }
		public Order? Order { get; set; }
		public int ItemQuntity { get; set; }
	}
}