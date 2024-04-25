using Movie.Interfaces;
using Movie.Models;

namespace Movie.Repositories
{
	public class UserRepository : IUserRepository
	{
		public Task<bool> DeleteUserAsync(User user)
		{
			throw new NotImplementedException();
		}

		public Task EditUserAsync(User user)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetUserByIdAsync(long id)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<User>> GetUsersAsync()
		{
			throw new NotImplementedException();
		}

		public Task<bool> SaveAsync()
		{
			throw new NotImplementedException();
		}

		public Task<bool> UserExistsAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
