

namespace Application
{
    // Projet: Application/IUserHandler.cs (Interface du port)
    public interface IUserHandler
    {
        // C
        int Create(UserDto userDto);

        public Task<SecurityUserDto> GetSecurityUserByEmailAsync(string Email);

        public Task<UserDto> GetUserByEmailAsync(string Email);

        // R
        UserDto GetById(int id);
        IEnumerable<UserDto> GetAll();
        // U
        void Update(int id, UserDto userDto);
        // D
        void Delete(int id);
    }
}
