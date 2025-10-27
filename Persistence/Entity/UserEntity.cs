

namespace Persistence
{
    // Dans Models/User.cs ou Entités/User.cs
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; } // Ajouté
        public string LastName { get; set; }  // Ajouté
        public byte[] PasswordHash { get; set; } // Pour le mot de passe hashé
        public byte[] PasswordSalt { get; set; } // Pour le sel (salt)
        public string Role { get; set; } // Ex: "Admin", "User"
    }
}
