namespace Shop.Domain.Entities
{
	public class Order : BaseEntity
	{
		public Guid CartId { get; set; }
		public Cart? Cart { get; set; }
		public List<OrderItem>? OrderItems { get; set; }
		public DateTime? OrderDate { get; set; }
		public string? Status { get; set; }
	}
}