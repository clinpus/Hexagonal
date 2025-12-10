

namespace Domain
{

    public class Customer
    {
        // L'Id est de type int et le 'private set' permet à EF Core de l'initialiser.
        public int Id { get; private set; }

        // Autres propriétés comme avant
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Adresse{ get; private set; }
        public string Tel{ get; private set; }
        public string Email { get; private set; }
        public string NumeroSiret { get; private set; } 
        public DateTime CreationDate { get; private set; }
        public bool EstProfessionnel => !string.IsNullOrEmpty(NumeroSiret);

        private readonly List<Invoice> _Invoices = new();
        public IReadOnlyList<Invoice> Invoices => _Invoices.AsReadOnly();

        // **********************************
        // 1. CONSTRUCTEUR (Pour l'instanciation de l'entité)
        // L'Id est 0 car il n'est pas encore attribué par la BDD.
        // **********************************
        private Customer(string lastName, string firstName, string email, string siret)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            NumeroSiret = siret;
        }

        // **********************************
        // 2. FABRIQUE STATIQUE (Méthode de création contrôlée)
        // L'Id n'est pas passé en paramètre car il est géré par la persistance.
        // **********************************
        public static Customer Creer(int Id, string lastName,string firstName, string email, string numeroSiret = null)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Le nom du client est obligatoire.", nameof(lastName));

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("L'adresse e-mail n'est pas valide.", nameof(email));

            // Création avec Id = 0 (en attente de la BDD)
            Customer rCustomer = new Customer(lastName, firstName, email, numeroSiret);
            rCustomer.Id = Id;
            return rCustomer;
        }

        // **********************************
        // 3. MÉTHODES DE DOMAINE
        // **********************************
        public void MettreAJourCoordonnees(string nouveauNom, string nouvelEmail)
        {
            // Logique de validation et mise à jour...
            LastName = nouveauNom;
            FirstName = nouveauNom;
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
