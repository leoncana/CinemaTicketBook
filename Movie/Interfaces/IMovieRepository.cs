using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Movie.Models;

namespace Movie.Interfaces
{
	public interface IMovieRepository
	{
		Task<bool> AddMovieAsync(Moviee movie);
		Task<ICollection<Moviee>> GetAllMoviesAsync();
		Task<Moviee> GetMovieByIdAsync(long id);
		Task<bool> EditMovieAsync(Moviee movie);
		Task<bool> DeleteMovieAsync(Moviee movie);
		Task<ICollection<Moviee>> SearchMoviesAsync(string searchCharacter);
		Task<bool> MovieExistsAsync(Moviee movie);
		Task<bool> UploadMoviePictureAsync(long movieId, IFormFile portraitPhoto, IFormFile landscapePhoto);
		Task<bool> SaveAsync();
	}
}
