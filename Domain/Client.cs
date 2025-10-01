using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain
{

    public class Client
    {
        // L'Id est de type int et le 'private set' permet à EF Core de l'initialiser.
        public int Id { get; private set; }

        // Autres propriétés comme avant
        public string Nom { get; private set; }
        public string Email { get; private set; }
        public string NumeroSiret { get; private set; }

        public bool EstProfessionnel => !string.IsNullOrEmpty(NumeroSiret);

        private readonly List<Invoice> _Invoices = new();
        public IReadOnlyList<Invoice> Invoices => _Invoices.AsReadOnly();

        // **********************************
        // 1. CONSTRUCTEUR (Pour l'instanciation de l'entité)
        // L'Id est 0 car il n'est pas encore attribué par la BDD.
        // **********************************
        private Client(string nom, string email, string siret)
        {
            // Id = 0 (valeur par défaut pour les nouveaux objets)
            Nom = nom;
            Email = email;
            NumeroSiret = siret;
        }

        // **********************************
        // 2. FABRIQUE STATIQUE (Méthode de création contrôlée)
        // L'Id n'est pas passé en paramètre car il est géré par la persistance.
        // **********************************
        public static Client Creer(int Id, string nom, string email, string numeroSiret = null)
        {
            if (string.IsNullOrWhiteSpace(nom))
                throw new ArgumentException("Le nom du client est obligatoire.", nameof(nom));

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("L'adresse e-mail n'est pas valide.", nameof(email));

            // Création avec Id = 0 (en attente de la BDD)
            Client rClient = new Client(nom, email, numeroSiret);
            rClient.Id = Id;
            return rClient;
        }

        // **********************************
        // 3. MÉTHODES DE DOMAINE
        // **********************************
        public void MettreAJourCoordonnees(string nouveauNom, string nouvelEmail)
        {
            // Logique de validation et mise à jour...
            Nom = nouveauNom;
            Email = nouvelEmail;
        }

        public void AjouterInvoice(Invoice Invoice)
        {
            if (Invoice == null)
                throw new ArgumentNullException(nameof(Invoice));

            _Invoices.Add(Invoice);
        }

    

    }
}
