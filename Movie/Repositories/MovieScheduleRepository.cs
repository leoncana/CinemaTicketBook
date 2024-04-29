using Microsoft.EntityFrameworkCore;
using Movie.Data;
using Movie.Interfaces;
using Movie.Models;

namespace Movie.Repositories
{
	public class MovieScheduleRepository : IMovieScheduleRepository
	{
		private readonly ApplicationDbContext _context;

		public MovieScheduleRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> AddMovieScheduleAsync(MovieSchedule movieSchedule)
		{
			_context.MovieSchedules.Add(movieSchedule);
			var created = await _context.SaveChangesAsync();
			return created > 0;
		}

		public async Task<ICollection<MovieSchedule>> GetAllMovieSchedulesAsync()
		{
			return await _context.MovieSchedules.Include(ms => ms.NotAvailableSeats).ToListAsync();
		}

		public async Task<MovieSchedule> GetMovieScheduleById(long id)
		{
			try
			{
				return await _context.MovieSchedules.Include(m => m.NotAvailableSeats).FirstOrDefaultAsync(m => m.MovieScheduleId == id);
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to retrieve movie: {ex.Message}");
			}
		}


		public async Task<ICollection<MovieSchedule>> GetScheduleByMovieAsync(long screenid, DateTime date, long movieid)
		{
			var screen = await _context.Screens.Include(s => s.MovieSchedules).FirstOrDefaultAsync(s => s.Id == screenid);
			if (screen == null)
			{
				throw new Exception("Screen not found");
			}

			var queryDate = date.Date;
			var movieSchedules = screen.MovieSchedules.Where(schedule =>
			{
				var scheduleDate = schedule.ShowDate.Date;
				return scheduleDate == queryDate && schedule.MovieId == movieid;
			}).ToList();

			if (movieSchedules.Count == 0)
			{
				throw new Exception("Movie schedule not found");
			}

			return (movieSchedules);
		}

	}
}
