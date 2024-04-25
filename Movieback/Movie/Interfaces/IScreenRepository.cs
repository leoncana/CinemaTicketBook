using Movie.Models;

namespace Movie.Interfaces
{
	public interface IScreenRepository
	{
		Task AddScreenAsync(Screen screen);
		Task<Screen> GetScreenByIdAsync(long id);
		Task<ICollection<Screen>> GetScreensAsync();

		Task<ICollection<Screen>> GetScreensByCityAsync(long cityId);
		Task<ICollection<Screen>> GetScreensByMovieScheduleAsync(long cityId, DateTime date, string movieId);
		Task EditScreenAsync(Screen screen);
		Task<bool> DeleteScreenAsync(Screen screen);

		Task<bool> ScreenExistsAsync(int id);
		Task<bool> SaveAsync();
	}
}
