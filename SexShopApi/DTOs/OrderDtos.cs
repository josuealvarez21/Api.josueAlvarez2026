namespace SexShopApi.DTOs
{
    public class CreateOrderDetailDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateOrderDto
    {
        public List<CreateOrderDetailDto> OrderDetails { get; set; } = new();
    }

    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal => Quantity * UnitPrice;
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderDetailDto> Details { get; set; } = new();
    }
}
