using Movie.Dto.User.UserResponse;
using Movie.Models;

namespace Movie.Interfaces
{
	public interface IUserRepository
	{
		Task<User> ValidateCredentials(string email, string password);
		Task<UserDto> GetUserByUsernameAsync(string username);
		Task<string> Login(User user);
		Task<bool> RegisterUser(User user);
		string GenerateTokenString(User user);
		Task CreateAdminUsers();
	}
}
