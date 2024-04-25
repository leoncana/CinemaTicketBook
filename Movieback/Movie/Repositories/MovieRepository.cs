using Microsoft.EntityFrameworkCore;
using Movie.Data;
using Movie.Interfaces;
using Movie.Models;

namespace Movie.Repositories
{
	public class MovieRepository : IMovieRepository
	{
		private readonly ApplicationDbContext _context;

		public MovieRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> AddMovieAsync(Moviee movie)
		{
			try
			{
				if (await MovieExistsAsync(movie))
				{
					throw new Exception("This movie already exists.");
				}
				_context.Movies.Add(movie);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to add movie: {ex.Message}");
			}
		}

		public async Task<ICollection<Moviee>> GetAllMoviesAsync()
		{
			try
			{
				return await _context.Movies.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to retrieve movies: {ex.Message}");
			}
		}


		public async Task<Moviee> GetMovieByIdAsync(long id)
		{
			try
			{
				return await _context.Movies.Include(m => m.MovieSchedules).Include(m => m.MovieCelebs).ThenInclude(m => m.Celeb).FirstOrDefaultAsync(m => m.Id == id);
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to retrieve movie: {ex.Message}");
			}
		}

		//public async Task<MovieSchedule> GetScheduleByMovieIdAsync(long id)
		//{
		//	try
		//	{
		//		return await _context.MovieSchedules.FirstOrDefaultAsync(ms => ms.MovieId == id);
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new Exception($"Failed to retrieve schedule: {ex.Message}");
		//	}
		//}

		public async Task<bool> EditMovieAsync(Moviee movie)
		{
			try
			{
				_context.Movies.Update(movie);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to edit movie: {ex.Message}");
			}
		}


		public async Task<bool> DeleteMovieAsync(Moviee movie)
		{
			try
			{
				_context.Movies.Remove(movie);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to delete movie: {ex.Message}");
			}
		}


		public async Task<bool> AddCelebToMovieAsync(long movieId, string celebType, string celebName, string celebRole, string celebImage)
		{
			try
			{
				var movie = await _context.Movies.Include(m => m.MovieCelebs).FirstOrDefaultAsync(m => m.Id == movieId);
				if (movie == null)
				{
					throw new ArgumentException("Movie not found");
				}

				var existingCeleb = await _context.Celebs.FirstOrDefaultAsync(c => c.CelebName == celebName && c.CelebRole == celebRole);
				if (existingCeleb == null)
				{
					existingCeleb = new Celeb
					{
						CelebName = celebName,
						CelebType = celebType,
						CelebRole = celebRole,
						CelebImage = celebImage
					};
					_context.Celebs.Add(existingCeleb);
				}

				if (movie.MovieCelebs == null)
				{
					movie.MovieCelebs = new List<MovieCeleb>();
				}

				if (!movie.MovieCelebs.Any(mc => mc.CelebId == existingCeleb.Id))
				{
					var movieCeleb = new MovieCeleb
					{
						MovieId = movieId,
						CelebId = existingCeleb.Id,
						Celeb = existingCeleb
					};

					movie.MovieCelebs.Add(movieCeleb);
				}
				else
				{
					throw new ArgumentException("Celebrity is already linked to the movie");
				}

				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to add celeb to movie: {ex.Message}");
			}
		}


		public async Task<bool> MovieExistsAsync(Moviee movie)
		{
			try
			{
				return await _context.Movies.AnyAsync(m => m.Title == movie.Title
				&& m.Description == movie.Description
				);
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to check movie existence: {ex.Message}");
			}
		}


		public async Task<bool> SaveAsync()
		{
			try
			{
				var saved = await _context.SaveChangesAsync();
				return saved > 0;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to save changes: {ex.Message}");
			}
		}
	}
}
