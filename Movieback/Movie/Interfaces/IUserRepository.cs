using Movie.Models;

namespace Movie.Interfaces
{
	public interface IUserRepository
	{
		Task<ICollection<User>> GetUsersAsync();
		Task<User> GetUserByIdAsync(long id);
		Task EditUserAsync(User user);
		Task<bool> DeleteUserAsync(User user);

		Task<bool> UserExistsAsync(int id);
		Task<bool> SaveAsync();
	}
}
