
using Persistence.Entity;

namespace Persistence
{
    public class OrderItemEntity
    {
        public int OrderId { get; set; }

        public int Id { get;  set; }
        
        public int ProductEntityId { get;  set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public OrderEntity Order { get; set; }

        public ProductEntity Product { get; private set; }

    }
    
}
