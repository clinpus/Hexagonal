using Domain;

namespace Application
{
    public interface IAuthService
    {
        //public string CreateToken(User user);
        public (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password);
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        string CreateToken(UserDto user);
    }
}
