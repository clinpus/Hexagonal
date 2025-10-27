using Domain;
using Microsoft.EntityFrameworkCore;


namespace Persistence
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public User GetById(int id)
        {
            return MapToDomain(_context.Users.Where(x => x.Id == id).FirstOrDefault());
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var _user = _context.Users.Where(x => x.Email == email).FirstOrDefault();
            return MapToDomain(_user);
        }


       
        public int  Create(User User)
        {
            var entity = MapToEntity(User);
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void Update(User user)
        {
            var entityToPersist = MapToEntity(user);
            // **LIGNE DE CORRECTION** : Détache l'ancienne version s'il en existe une
            var local = _context.Set<UserEntity>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entityToPersist.Id));
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Users.Update(entityToPersist);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            var entitys = _context.Users;

            if (entitys == null)
            {
                return null; // Retourne null si l'entité n'existe pas en BDD
            }

            List<User> lstInvocies = new List<User>();
            foreach (var entity in entitys)
            {
                lstInvocies.Add(MapToDomain(entity));
            }
            return lstInvocies;
        }

        private UserEntity MapToEntity(User User)
        {

            var entity = new UserEntity()
            {
                Id = User.Id,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                PasswordHash = User.PasswordHash,
                PasswordSalt = User.PasswordSalt,
                Role = User.Role
            };
            return entity;
        }

        private User MapToDomain(UserEntity UserEntity)
        {
            if(UserEntity == null)
            {
                return null;
            }
            User rUser = User.Creer(
                UserEntity.Id,
                UserEntity.LastName,
                UserEntity.Email,
                UserEntity.Role,
                UserEntity.PasswordHash,
                UserEntity.PasswordSalt
            );
            return rUser;
        }
        
    }
}
