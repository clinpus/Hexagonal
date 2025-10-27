
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        // L'instance est injectée via le constructeur par le framework
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Dans Services/AuthService.cs (Simplifié)
        public string CreateToken(UserDto user)
        {
            // 1. Définir les Claims (informations sur l'utilisateur, ex: Id, Role)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // 2. Créer les clés et les informations du Token (Signature)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])); 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // 3. Créer le Token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), // Expiration
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        /// <summary>
        /// Crée le hash (mot de passe haché) et le sel (salt) pour un mot de passe donné.
        /// </summary>
        public (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
        {
            // Utilise l'algorithme sécurisé HMACSHA512
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                // Le sel (salt) est généré aléatoirement et est stocké dans hmac.Key
                byte[] passwordSalt = hmac.Key;

                // Calcule le hachage en utilisant le mot de passe et le sel
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                // 4. Renvoi du tuple
                return (passwordHash, passwordSalt);
            }
            
        }


        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            // Recrée l'objet HMAC avec le sel (storedSalt)
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                // Calcule le hachage pour le mot de passe fourni
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Compare les deux tableaux de bytes
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false; // Échec de la correspondance
                }
            }
            return true; // Correspondance réussie
        }
    }
}
