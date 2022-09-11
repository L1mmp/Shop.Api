namespace Shop.Domain.Dtos
{
	public class OrderDto
	{
		private decimal totalPrice;

		public List<OrderItemDto>? OrderItemDtos { get; set; }
		public decimal TotalPrice
		{
			get => totalPrice;
			set => totalPrice = value;
		}

	//[TODO]: Make total order price 
	//	var total = 0m;
	//
	//		foreach (var orderItem in OrderItemDtos)
	//		{
	//			total += (orderItem.ItemQuntity * orderItem.ItemDto.Price);
	//		}
	//
	//	totalPrice = total;
	}
}