using Microsoft.AspNetCore.Mvc;
using Movie.Models;

namespace Movie.Interfaces
{
	public interface IMovieRepository
	{
		Task<bool> AddMovieAsync(Moviee movie);
		Task<ICollection<Moviee>> GetAllMoviesAsync();
		Task<Moviee> GetMovieByIdAsync(long id);
		//Task<MovieSchedule> GetScheduleByMovieIdAsync(long id);
		Task<bool> EditMovieAsync(Moviee movie);
		Task<bool> DeleteMovieAsync(Moviee movie);
		Task<bool> AddCelebToMovieAsync(long movieId, string celebType, string celebName, string celebRole, string celebImage);
		Task<bool> MovieExistsAsync(Moviee movie);
		Task<bool> SaveAsync();
	}
}
