

namespace Domain
{
    public class OrderItem
    {
        public int Id { get; private set; }
        public int OrderId { get; private set; } // Clé étrangère vers la racine
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        // -----------------------------------------------------------------
        // CONSTRUCTEURS
        // -----------------------------------------------------------------

        // Constructeur privé pour EF Core / ORM
        private OrderItem() { }

        // Constructeur utilisé par la méthode Order.AddItem()
        internal OrderItem(int orderId, int productId, string productName, decimal price, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }
    }
    
}
