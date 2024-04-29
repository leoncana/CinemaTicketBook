using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Dto.Movie.MovieRequest;
using Movie.Dto.Movie.MovieResponse;
using Movie.Interfaces;
using Movie.Models;
using Movie.Repositories;

namespace Movie.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		private readonly IMovieRepository _movieRepository;
		private readonly IMapper _mapper;

		public MoviesController(IMovieRepository movieRepository, IMapper mapper)
		{
			_movieRepository = movieRepository;
			_mapper = mapper;
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> AddMovie([FromForm] MovieCreateDto movieDto)
		{
			try
			{
				if (movieDto == null)
				{
					return BadRequest("Movie object is null");
				}

				if (!Enum.IsDefined(typeof(Genres), movieDto.Genre))
				{
					return BadRequest($"Invalid genre: {movieDto.Genre}");
				}
				var mappedMovie = _mapper.Map<Moviee>(movieDto);
				await _movieRepository.AddMovieAsync(mappedMovie);


				long movieId = mappedMovie.Id;

				if (movieDto.PortraitPicture != null)
				{
					var uploadResult = await _movieRepository.UploadMoviePictureAsync(movieId, movieDto.PortraitPicture, movieDto.LandscapePicture);
					if (!uploadResult)
					{
						return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading movie pictures");
					}
				}

				return StatusCode(StatusCodes.Status201Created, "Movie created successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding movie: {ex.Message}");
			}
		}



		[HttpGet]
		public async Task<IActionResult> GetAllMovies()
		{
			try
			{
				var movies = await _movieRepository.GetAllMoviesAsync();
				var movieDtos = new List<ViewAllMoviesDto>();
				foreach (var movie in movies)
				{
					var movieDto = _mapper.Map<ViewAllMoviesDto>(movie);
					if (!string.IsNullOrEmpty(movie.PortraitImagePath) || !string.IsNullOrEmpty(movie.LandscapeImagePath))
					{
						byte[] portraitImageBytes = await System.IO.File.ReadAllBytesAsync(movie.PortraitImagePath);
						string base64PortraitImage = $"data:image/png;base64,{Convert.ToBase64String(portraitImageBytes)}";
						movieDto.PortraitImgUrl = base64PortraitImage;

						byte[] landscapeImageBytes = await System.IO.File.ReadAllBytesAsync(movie.LandscapeImagePath);
						string base64LandscapeImage = $"data:image/png;base64,{Convert.ToBase64String(landscapeImageBytes)}";
						movieDto.LandscapeImgUrl = base64LandscapeImage;
					}
					else
					{
						movieDto.PortraitImgUrl = "";
						movieDto.LandscapeImgUrl = "";
					}
					movieDtos.Add(movieDto);
				}
				return Ok(movieDtos);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving movies: {ex.Message}");
			}
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetMovieById(long id)
		{
			try
			{
				var movie = await _movieRepository.GetMovieByIdAsync(id);
				if (movie == null)
				{
					return NotFound($"Movie with ID {id} not found");
				}
				else
				{
					var movieDto = _mapper.Map<ViewMovieByIdDto>(movie);
					if (!string.IsNullOrEmpty(movie.PortraitImagePath) || !string.IsNullOrEmpty(movie.LandscapeImagePath))
					{
						byte[] portraitImageBytes = await System.IO.File.ReadAllBytesAsync(movie.PortraitImagePath);
						string base64PortraitImage = $"data:image/png;base64,{Convert.ToBase64String(portraitImageBytes)}";
						movieDto.PortraitImgUrl = base64PortraitImage;

						byte[] landscapeImageBytes = await System.IO.File.ReadAllBytesAsync(movie.LandscapeImagePath);
						string base64LandscapeImage = $"data:image/png;base64,{Convert.ToBase64String(landscapeImageBytes)}";
						movieDto.LandscapeImgUrl = base64LandscapeImage;
					}
					else
					{
						movieDto.PortraitImgUrl = "";
						movieDto.LandscapeImgUrl = "";
					}
					return Ok(movieDto);
				}
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving movie: {ex.Message}");
			}
		}

		[Authorize(Roles = "Admin")]

		[HttpPut("{id}")]
		public async Task<IActionResult> EditMovie(long id, [FromBody] MovieEditDto movieDto)
		{
			try
			{
				if (movieDto == null || id != movieDto.Id)
				{
					return BadRequest("Invalid movie object");
				}


				var existingMovie = await _movieRepository.GetMovieByIdAsync(id);
				if (existingMovie == null)
				{
					return NotFound($"Movie with ID {id} not found");
				}

				existingMovie.Title = movieDto.Title;
				existingMovie.Description = movieDto.Description;
				//existingMovie.PortraitImgUrl = movieDto.PortraitImgUrl;
				//existingMovie.LandscapeImgUrl = movieDto.LandscapeImgUrl;
				existingMovie.Rating = movieDto.Rating;
				existingMovie.Duration = movieDto.Duration;
				return Ok($"Movie with ID {id} updated successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating movie: {ex.Message}");
			}
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMovie(long id)
		{
			try
			{
				var movieToDelete = await _movieRepository.GetMovieByIdAsync(id);
				if (movieToDelete == null)
				{
					return NotFound($"Movie with ID {id} not found");
				}

				await _movieRepository.DeleteMovieAsync(movieToDelete);

				return Ok($"Movie with ID {id} deleted successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting movie: {ex.Message}");
			}
		}


		[HttpGet("search/{searchCharacter}")]
		public async Task<IActionResult> SearchMovies(string searchCharacter)
		{
			try
			{
				var movies = await _movieRepository.SearchMoviesAsync(searchCharacter);
				var movieDtos = new List<ViewAllMoviesDto>();
				foreach (var movie in movies)
				{
					var movieDto = _mapper.Map<ViewAllMoviesDto>(movie);
					if (!string.IsNullOrEmpty(movie.PortraitImagePath) || !string.IsNullOrEmpty(movie.LandscapeImagePath))
					{
						byte[] portraitImageBytes = await System.IO.File.ReadAllBytesAsync(movie.PortraitImagePath);
						string base64PortraitImage = $"data:image/png;base64,{Convert.ToBase64String(portraitImageBytes)}";
						movieDto.PortraitImgUrl = base64PortraitImage;

						byte[] landscapeImageBytes = await System.IO.File.ReadAllBytesAsync(movie.LandscapeImagePath);
						string base64LandscapeImage = $"data:image/png;base64,{Convert.ToBase64String(landscapeImageBytes)}";
						movieDto.LandscapeImgUrl = base64LandscapeImage;
					}
					else
					{
						movieDto.PortraitImgUrl = "";
						movieDto.LandscapeImgUrl = "";
					}
					movieDtos.Add(movieDto);
				}
				return Ok(movieDtos);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error searching movies: {ex.Message}");
			}
		}
	}
}
