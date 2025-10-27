
namespace Application
{
    public enum OrderStatus
    {
        Pending,
        Paid,
        Shipped,
        Delivered,
        Cancelled
    }

    public class OrderDto
    {
        // Propriétés de l'Entité (Identité Unique)
        public int Id { get; private set; }

        // Relation avec Customer (Référence par ID, bonne pratique DDD)
        public int CustomerId { get; private set; }

        // Propriétés de l'Agrégat
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalPrice { get; private set; }

        // Collection des Entités Internes (Seul l'Order peut les modifier)
        private readonly List<OrderItemDto> _items = new List<OrderItemDto>();
        public IReadOnlyCollection<OrderItemDto> Items => _items.AsReadOnly();



    }
    
}
