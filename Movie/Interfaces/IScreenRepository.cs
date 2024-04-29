using Movie.Models;

namespace Movie.Interfaces
{
	public interface IScreenRepository
	{
		Task<bool> AddScreenAsync(Screen screen);
		Task<ICollection<Screen>> GetAllScreensAsync();
		Task<Screen> GetScreenByIdAsync(long id);
		Task<bool> EditScreenAsync(Screen screen);
		Task<bool> DeleteScreenAsync(Screen screen);
		Task<ICollection<Screen>> GetScreensByCityAsync(string city);
		Task<ICollection<Screen>> GetScreensByMovieScheduleAsync(string city, long movieId, DateTime date);
		Task<ICollection<Screen>> GetScreensByDateAsync(DateTime date);

		Task<bool> ScreenExistsAsync(string name);
		Task<bool> SaveAsync();
	}
}
