

namespace Application
{
    public class OrderItemDto
    {
        public int Id { get; private set; }
        public int OrderId { get; private set; } // Clé étrangère vers la racine
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

    }
    
}
