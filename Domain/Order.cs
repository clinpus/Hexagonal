namespace Domain
{
    public enum OrderStatus
    {
        Pending,
        Paid,
        Shipped,
        Delivered,
        Cancelled
    }

    public class Order
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
        private readonly List<OrderItem> _items = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();


        // Constructeur privé pour EF Core / ORM
        private Order()
        {
            Status = OrderStatus.Pending; // Initialisation par défaut
        }

        /// <summary>
        /// Crée une nouvelle commande pour un client donné.
        /// </summary>
        /// <param name="customerId">L'identifiant du client.</param>
        public Order(int customerId) : this()
        {

            CustomerId = customerId;
            OrderDate = DateTime.UtcNow;
            TotalPrice = 0m;
        }

        // -----------------------------------------------------------------
        // MÉTHODES DE COMPORTEMENT
        // -----------------------------------------------------------------

        /// <summary>
        /// Ajoute un article à la commande et met à jour le prix total.
        /// </summary>
        public void AddItem(int productId, string productName, decimal price, int quantity)
        {
            if (Status != OrderStatus.Pending)
            {
                throw new InvalidOperationException("Impossible d'ajouter des articles à une commande qui n'est pas en attente.");
            }

            // 1. Création de l'Entité interne
            var newItem = new OrderItem(Id, productId, productName, price, quantity);

            // 2. Mise à jour de l'état de l'agrégat
            _items.Add(newItem);
            CalculateTotalPrice();
        }

        /// <summary>
        /// Confirme le paiement et change le statut.
        /// </summary>
        public void MarkAsPaid()
        {
            if (Status != OrderStatus.Pending)
            {
                throw new InvalidOperationException("La commande n'est pas en attente de paiement.");
            }

            Status = OrderStatus.Paid;
            // Logique métier : Envoyer notification, déclencher le processus de livraison, etc.
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = _items.Sum(item => item.Quantity * item.Price);
        }
    }
    
}
