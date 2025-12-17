using Domain;

namespace Persistence
{
    public class CustomerEntity
    {
        // Clé primaire auto-incrémentée
        public int Id { get; set; }

        // Propriétés de base
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Adresse { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string NumeroSiret { get; set; }

        // Navigation Property pour la relation one-to-many
        // Cette collection est chargée par EF Core
        public ICollection<InvoiceEntity> Invoices { get; set; }
        public ICollection<OrderEntity> Orders { get; set; }

        // Méthodes de mapping (souvent dans une classe séparée, mais ici pour l'exemple)
        public static CustomerEntity FromDomain(Customer client)
        {
            return new CustomerEntity
            {
                Id = client.Id, // Si le client existe déjà dans la BDD
                LastName = client.LastName,
                Email = client.Email,
                NumeroSiret = client.NumeroSiret
            };
        }

        public Customer ToDomain()
        {
            // On ne peut pas créer directement une entité de domaine avec son Id,
            // donc on utiliserait une méthode de fabrique ou un service de mapping plus sophistiqué
            // pour re-créer une entité de domaine à partir de l'entité de persistance.
            throw new NotImplementedException("Le mapping de CustomerEntity vers Customer nécessite un service de mapping plus complet.");
        }
    }
}
