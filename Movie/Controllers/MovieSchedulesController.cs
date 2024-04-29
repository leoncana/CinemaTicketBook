using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Dto.Booking.BookingResponse;
using Movie.Dto.MovieSchedule.MovieScheduleRequest;
using Movie.Dto.MovieSchedule.MovieScheduleResponse;
using Movie.Interfaces;
using Movie.Models;


[Authorize]
[ApiController]
[Route("[controller]")]
public class MovieScheduleController : ControllerBase
{
	private readonly IMovieScheduleRepository _movieSchedulerepository;
	private readonly IMapper _mapper;

	public MovieScheduleController(IMovieScheduleRepository movieSchedulerepository, IMapper mapper)
	{
		_movieSchedulerepository = movieSchedulerepository;
		_mapper = mapper;
	}

	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> AddMovieSchedule([FromBody] MovieScheduleCreateDto movieScheduleDto)
	{
		try
		{
			var movieSchedule = _mapper.Map<MovieSchedule>(movieScheduleDto);
			var success = await _movieSchedulerepository.AddMovieScheduleAsync(movieSchedule);

			if (success)
				return Ok(new { message = "Movie Schedule added successfully" });
			else
				return BadRequest(new { message = "Failed to add Movie Schedule" });
		}
		catch (Exception)
		{
			return BadRequest(new { message = "An error occurred while processing your request." });
		}
	}


	[HttpGet]
	public async Task<IActionResult> GetMovieSchedules()
	{
		try
		{
			var movieSchedules = await _movieSchedulerepository.GetAllMovieSchedulesAsync();
			var movieScheduleDtos = new List<MovieScheduleDto>();
			foreach (var movieSchedule in movieSchedules)
			{
				var movieScheduleDto = _mapper.Map<MovieScheduleDto>(movieSchedule);
				movieScheduleDtos.Add(movieScheduleDto);
			}
			return Ok(movieScheduleDtos);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
		}
	}


	[HttpGet("{id}")]
	public async Task<IActionResult> GetMovieScheduleById(long id)
	{
		try
		{
			var movieSchedule = await _movieSchedulerepository.GetMovieScheduleById(id);

			var movieDto = _mapper.Map<MovieScheduleDto>(movieSchedule);
			if (movieDto == null)
				return NotFound(new { message = "Schedule not found" });
			else
				return Ok(movieDto);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
		}
	}


	[HttpGet("getScheduleBymovie/{screenid}/{date}/{movieid}")]
	public async Task<IActionResult> GetScheduleByMovieAsync(long screenid, string date, long movieid)
	{
		try
		{
			DateTime parsedDate;
			if (!DateTime.TryParse(date, out parsedDate))
			{
				return BadRequest(new { success = false, message = "Invalid date format" });
			}

			var movieSchedule = await _movieSchedulerepository.GetScheduleByMovieAsync(screenid, parsedDate, movieid);
			var movieSchedulesDto = _mapper.Map<List<MovieScheduleDto>>(movieSchedule);
			return Ok(movieSchedulesDto);
		}
		catch (Exception ex)
		{
			if (ex.Message == "Screen not found" || ex.Message == "Movie schedule not found")
			{
				return NotFound(new { success = false, message = ex.Message });
			}
			return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
		}
	}


}