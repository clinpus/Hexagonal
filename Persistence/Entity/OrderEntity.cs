
namespace Persistence
{
    public enum OrderStatus
    {
        Pending,
        Paid,
        Shipped,
        Delivered,
        Cancelled
    }

    public class OrderEntity
    {
        // Propriétés de l'Entité (Identité Unique)
        public int Id { get; set; }

        // Propriétés de l'Agrégat
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalPrice { get; private set; }


        // *** 3. CLÉ ÉTRANGÈRE ***
        // Clé étrangère vers l'entité CustomerEntity
        public int CustomerId { get; set; }

        // *** 4. PROPRIÉTÉ DE NAVIGATION (Relations) ***

        // Relation One-to-Many : Une Invoice appartient à un Customer
        // EF Core la remplit lors du chargement
        public CustomerEntity Customer { get; set; }


        // Relation One-to-Many : Une Invoice a plusieurs Lignes
        // ICollection est le type standard pour les collections de navigation dans EF Core
        public ICollection<OrderItemEntity> Items { get; set; }

        // Constructeur sans argument pour la désérialisation par EF Core
        public OrderEntity()
        {
            // Initialisation de la collection pour éviter les NullReferenceExceptions
            Items = new HashSet<OrderItemEntity>();
        }



    }
    
}
