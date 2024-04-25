using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Movie.Dto.Request;
using Movie.Dto.Response;
using Movie.Interfaces;
using Movie.Models;
using Movie.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Controllers
{
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

		[HttpPost]
		public async Task<IActionResult> AddMovie([FromBody] MovieCreateDto movieDto)
		{
			try
			{
				if (movieDto == null)
				{
					return BadRequest("Movie object is null");
				}

				var mappedMovie = _mapper.Map<Moviee>(movieDto);

				await _movieRepository.AddMovieAsync(mappedMovie);

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
				var movieDto = _mapper.Map<ViewMovieByIdDto>(movie);

				if (movie == null)
				{
					return NotFound($"Movie with ID {id} not found");
				}
				if (movie.MovieCelebs != null)
				{
					movieDto.Celebs = movie.MovieCelebs
						.Select(st => st.Celeb.CelebName)
						.ToList();
				}
				return Ok(movieDto);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving movie: {ex.Message}");
			}
		}


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
				existingMovie.PortraitImgUrl = movieDto.PortraitImgUrl;
				existingMovie.LandscapeImgUrl = movieDto.LandscapeImgUrl;
				existingMovie.Rating = movieDto.Rating;
				existingMovie.Duration = movieDto.Duration;
				existingMovie.GenreNames = movieDto.GenreNames;
				await _movieRepository.EditMovieAsync(existingMovie);

				return Ok($"Movie with ID {id} updated successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating movie: {ex.Message}");
			}
		}


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


		[HttpPost("addCelebToMovie")]
		public async Task<IActionResult> AddCelebToMovie(long movieId, string celebType, string celebName, string celebRole, string celebImage)
		{
			try
			{
				var result = await _movieRepository.AddCelebToMovieAsync(movieId, celebType, celebName, celebRole, celebImage);
				return Ok($"Celeb with ID {celebName} added to movie {movieId} successfully");

			}
			catch (Exception ex)
			{
				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
			}
		}
	}
}
