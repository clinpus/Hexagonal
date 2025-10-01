﻿using Domain;
// Projet: Facturation.Persistence

namespace Persistence.Entity
{
    public class ClientEntity
    {
        // Clé primaire auto-incrémentée
        public int Id { get; set; }

        // Propriétés de base
        public string Nom { get; set; }
        public string Email { get; set; }
        public string NumeroSiret { get; set; }

        // Navigation Property pour la relation one-to-many
        // Cette collection est chargée par EF Core
        public ICollection<InvoiceEntity> Invoices { get; set; }

        // Méthodes de mapping (souvent dans une classe séparée, mais ici pour l'exemple)
        public static ClientEntity FromDomain(Client client)
        {
            return new ClientEntity
            {
                Id = client.Id, // Si le client existe déjà dans la BDD
                Nom = client.Nom,
                Email = client.Email,
                NumeroSiret = client.NumeroSiret
            };
        }

        public Client ToDomain()
        {
            // On ne peut pas créer directement une entité de domaine avec son Id,
            // donc on utiliserait une méthode de fabrique ou un service de mapping plus sophistiqué
            // pour re-créer une entité de domaine à partir de l'entité de persistance.
            throw new NotImplementedException("Le mapping de ClientEntity vers Client nécessite un service de mapping plus complet.");
        }
    }
}
