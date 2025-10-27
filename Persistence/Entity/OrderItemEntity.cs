
namespace Persistence
{
    public class OrderItemEntity
    {
        public int OrderId { get; set; }

        public int Id { get;  set; }
        
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public OrderEntity Order { get; set; }

    }
    
}
