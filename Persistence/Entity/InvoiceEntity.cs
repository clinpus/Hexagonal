
using Domain;

namespace Persistence.Entity
{
    public class InvoiceEntity
    {
        // *** 1. CLÉ PRIMAIRE ***
        public int Id { get; set; }

        // *** 2. PROPRIÉTÉS DE BASE ***
        public string Numero { get; set; }
        public DateTime DateEmission { get; set; }
        public DateTime DateEcheance { get; set; }
        public string Etat { get; set; }

        // *** 3. CLÉ ÉTRANGÈRE ***
        // Clé étrangère vers l'entité ClientEntity
        public int ClientId { get; set; }

        // *** 4. PROPRIÉTÉ DE NAVIGATION (Relations) ***

        // Relation One-to-Many : Une Invoice appartient à un Client
        // EF Core la remplit lors du chargement
        public ClientEntity Client { get; set; }

        // Relation One-to-Many : Une Invoice a plusieurs Lignes
        // ICollection est le type standard pour les collections de navigation dans EF Core
        public ICollection<InvoiceLineEntity> InvoiceLines { get; set; }

        // Constructeur sans argument pour la désérialisation par EF Core
        public InvoiceEntity()
        {
            // Initialisation de la collection pour éviter les NullReferenceExceptions
            InvoiceLines = new HashSet<InvoiceLineEntity>();
        }

    }
}
