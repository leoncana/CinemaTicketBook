using Movie.Models;

namespace Movie.Interfaces
{
	public interface IMovieScheduleRepository
	{
		Task<bool> AddMovieScheduleAsync(MovieSchedule movieSchedule);
		Task<MovieSchedule> GetMovieScheduleById(long id);
		Task<ICollection<MovieSchedule>> GetAllMovieSchedulesAsync();
		Task<ICollection<MovieSchedule>> GetScheduleByMovieAsync(long screenid, DateTime date, long movieid);
	};
}
