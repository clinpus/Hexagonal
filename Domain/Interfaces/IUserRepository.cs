

namespace Domain
{
    public interface IUserRepository
    {
        // Ajout de la méthode de lecture (Port Sortant)
        User GetById(int id);
        Task<User> GetUserByEmailAsync(string email);
        IEnumerable<User> GetAll();
        int Create(User user);
        public void Update(User user);
    }
}
