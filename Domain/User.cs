

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; } // Ajouté
        public string LastName { get; set; }  // Ajouté
        public byte[] PasswordHash { get; set; } // Pour le mot de passe hashé
        public byte[] PasswordSalt { get; set; } // Pour le sel (salt)
        public string Role { get; set; } // Ex: "Admin", "User"


        private User(string lastName, string email, string role)
        {
            LastName = lastName;
            Email = email;
            Role = role;
        }

        private User(string lastName, string email, string role, byte[] passwordHach, byte[] passwordSalt)
        {
            LastName = lastName;
            Email = email;
            Role = role;
            PasswordHash = passwordHach;
            PasswordSalt = passwordSalt;
        }

        public static User Creer(int Id, string lastName, string email,string role, byte[] passwordHach, byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Le nom du client est obligatoire.", nameof(lastName));

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("L'adresse e-mail n'est pas valide.", nameof(email));


            if (passwordHach == null)
                throw new ArgumentException("Le password n'est pas valide.");

            // Création avec Id = 0 (en attente de la BDD)
            User rUser = new User(lastName, email, role, passwordHach, passwordSalt);
            rUser.Id = Id;
            return rUser;
        }

        public static User Creer(int Id, string lastName, string email, string role)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Le nom du client est obligatoire.", nameof(lastName));

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("L'adresse e-mail n'est pas valide.", nameof(email));

            // Création avec Id = 0 (en attente de la BDD)
            User rUser = new User(lastName, email, role);
            rUser.Id = Id;
            return rUser;
        }

    }
}
