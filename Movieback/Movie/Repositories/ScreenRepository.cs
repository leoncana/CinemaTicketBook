using Movie.Interfaces;
using Movie.Models;

namespace Movie.Repositories
{
	public class ScreenRepository : IScreenRepository
	{
		public Task AddScreenAsync(Screen screen)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteScreenAsync(Screen screen)
		{
			throw new NotImplementedException();
		}

		public Task EditScreenAsync(Screen screen)
		{
			throw new NotImplementedException();
		}

		public Task<Screen> GetScreenByIdAsync(long id)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<Screen>> GetScreensAsync()
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<Screen>> GetScreensByCityAsync(long cityId)
		{
			throw new NotImplementedException();
		}

		public Task<ICollection<Screen>> GetScreensByMovieScheduleAsync(long cityId, DateTime date, string movieId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> SaveAsync()
		{
			throw new NotImplementedException();
		}

		public Task<bool> ScreenExistsAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateScreenAsync(Screen screen)
		{
			throw new NotImplementedException();
		}
	}
}
