using Microsoft.CodeAnalysis;
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
				if (!Enum.IsDefined(typeof(Genres), movie.Genre))
				{
					throw new Exception("Invalid city.");
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
				return await _context.Movies.Include(m => m.MovieSchedules).FirstOrDefaultAsync(m => m.Id == id);
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to retrieve movie: {ex.Message}");
			}
		}

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

		public async Task<ICollection<Moviee>> SearchMoviesAsync(string searchCharacter)
		{
			var words = searchCharacter.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			var moviesList = await _context.Movies
				.Where(t => EF.Functions.Like(t.Title, searchCharacter + "%")
				)
				.OrderBy(t => t.Title)
				.ToListAsync();

			return moviesList;
		}

		public async Task<bool> UploadMoviePictureAsync(long movieId, IFormFile portraitPhoto, IFormFile landscapePhoto)
		{
			var movie = await _context.Movies.FindAsync(movieId);
			string imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
			Directory.CreateDirectory(imagesDirectory);
			string movieName = movie.Title;


			string portraitFileName = Guid.NewGuid().ToString() + " - " + movieName + Path.GetExtension(portraitPhoto.FileName);
			string portraitFullPath = Path.Combine(imagesDirectory, portraitFileName);

			if (!string.IsNullOrEmpty(movie.PortraitImagePath) && File.Exists(movie.PortraitImagePath))
			{
				File.Delete(movie.PortraitImagePath);
			}

			using (Stream stream = new FileStream(portraitFullPath, FileMode.Create))
			{
				await portraitPhoto.CopyToAsync(stream);
			}
			movie.PortraitImagePath = portraitFullPath;



			string landscapeFileName = Guid.NewGuid().ToString() + " - " + movieName + Path.GetExtension(landscapePhoto.FileName);
			string landscapeFullPath = Path.Combine(imagesDirectory, landscapeFileName);

			if (!string.IsNullOrEmpty(movie.LandscapeImagePath) && File.Exists(movie.LandscapeImagePath))
			{
				File.Delete(movie.LandscapeImagePath);
			}

			using (Stream stream = new FileStream(landscapeFullPath, FileMode.Create))
			{
				await landscapePhoto.CopyToAsync(stream);
			}
			movie.LandscapeImagePath = landscapeFullPath;

			_context.MoviePictures.Add(new MoviePicture { MovieId = movieId });
			return await SaveAsync();
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
