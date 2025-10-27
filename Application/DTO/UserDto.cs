

namespace Application
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } // Ajouté
        public string LastName { get; set; }  // Ajouté
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Ex: "Admin", "User"
    }

    public class SecurityUserDto
    {
        public byte[] PasswordHash { get; set; } // Pour le mot de passe hashé
        public byte[] PasswordSalt { get; set; } // Pour le sel (salt)
    }
}
