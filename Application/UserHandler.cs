using Domain;
using Infrastructure;

namespace Application
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserRepository _userRepository; 
        private readonly IAuthService _authService;

        public UserHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<UserDto> GetUserByEmailAsync(string Email)
        {
            var user = await _userRepository.GetUserByEmailAsync(Email);
            return MapToDto(user);
        }

        public async Task<SecurityUserDto> GetSecurityUserByEmailAsync(string Email)
        {
            var userEntity = await _userRepository.GetUserByEmailAsync(Email);
            if (userEntity == null)
            {
                return null;
            }
            return new SecurityUserDto
            {
                PasswordHash = userEntity.PasswordHash, 
                PasswordSalt = userEntity.PasswordSalt 
            };
        }

        public int Create(UserDto userDto)
        {
            var (hash, salt) = _authService.CreatePasswordHash(userDto.Password);
            User clt  = User.Creer(
                                        userDto.Id,
                                        userDto.LastName,
                                        userDto.Email,
                                        userDto.Role,
                                        hash,
                                        salt
                                        );

            return _userRepository.Create(clt); 
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetAll()
        {
            var lstUser = _userRepository.GetAll();

            List<UserDto> userDtos = new List<UserDto>();
            foreach (User user in lstUser)
            {
                userDtos.Add(MapToDto(user));
            }

            return userDtos;
        }

        public void Update(int id, UserDto userDto)
        {
            throw new NotImplementedException();
        }

        UserDto IUserHandler.GetById(int id)
        {
            var user = _userRepository.GetById(id);
            var userDto = MapToDto(user);
            return userDto;
        }

        private UserDto MapToDto(User user)
        {
            var entity = new UserDto()
            {
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                Role = user.Role
            };
            return entity;
        }
    }
}
