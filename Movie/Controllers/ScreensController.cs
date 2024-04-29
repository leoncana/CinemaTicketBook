using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Dto.Screen.ScreenRequest;
using Movie.Dto.Screen.ScreenResponse;
using Movie.Interfaces;
using Movie.Models;
using System.Data;

namespace Movie.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ScreensController : ControllerBase
	{
		private readonly IScreenRepository _screenRepository;
		private readonly IMapper _mapper;

		public ScreensController(IScreenRepository screenRepository, IMapper mapper)
		{
			_screenRepository = screenRepository;
			_mapper = mapper;
		}

		[Authorize(Roles = "Admin")]

		[HttpPost]
		public async Task<IActionResult> AddScreen([FromBody] ScreenCreateDto screenDto)
		{
			try
			{
				if (screenDto == null)
				{
					return BadRequest("Screen object is null");
				}
				var mappedScreen = _mapper.Map<Screen>(screenDto);
				await _screenRepository.AddScreenAsync(mappedScreen);

				return StatusCode(StatusCodes.Status201Created, "Screen created successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding screen: {ex.Message}");
			}
		}


		[HttpGet]
		public async Task<IActionResult> GetAllScreens()
		{
			try
			{
				var screens = await _screenRepository.GetAllScreensAsync();
				var screenDtos = new List<ViewAllScreensDto>();
				foreach (var screen in screens)
				{
					var screenDto = _mapper.Map<ViewAllScreensDto>(screen);
					screenDtos.Add(screenDto);
				}
				return Ok(screenDtos);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving screens: {ex.Message}");
			}
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetScreenById(long id)
		{
			try
			{
				var screen = await _screenRepository.GetScreenByIdAsync(id);
				var screenDto = _mapper.Map<ViewAllScreensDto>(screen);

				if (screen == null)
				{
					return NotFound($"Screen with ID {id} not found");
				}
				return Ok(screenDto);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving screen: {ex.Message}");
			}
		}


		[Authorize(Roles = "Admin")]
		[HttpPut("{id}")]
		public async Task<IActionResult> EditScreen(long id, [FromBody] Screen screenDto)
		{
			try
			{
				if (screenDto == null || id != screenDto.Id)
				{
					return BadRequest("Invalid screen object");
				}


				var existingScreen = await _screenRepository.GetScreenByIdAsync(id);
				if (existingScreen == null)
				{
					return NotFound($"Screen with ID {id} not found");
				}

				existingScreen.Name = screenDto.Name;
				existingScreen.City = screenDto.City;
				await _screenRepository.EditScreenAsync(existingScreen);

				return Ok($"Screen with ID {id} updated successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating screen: {ex.Message}");
			}
		}


		[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteScreen(long id)
		{
			try
			{
				var screenToDelete = await _screenRepository.GetScreenByIdAsync(id);
				if (screenToDelete == null)
				{
					return NotFound($"Screen with ID {id} not found");
				}

				await _screenRepository.DeleteScreenAsync(screenToDelete);

				return Ok($"Screen with ID {id} deleted successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting screen: {ex.Message}");
			}
		}

		// In ScreensController
		[HttpGet("getByCity/")]
		public async Task<IActionResult> GetScreensByCity(string city)
		{
			try
			{
				var screens = await _screenRepository.GetScreensByCityAsync(city);
				var screenDtos = new List<ViewAllScreensDto>();
				foreach (var screen in screens)
				{
					var screenDto = _mapper.Map<ViewAllScreensDto>(screen);
					screenDtos.Add(screenDto);
				}
				return Ok(screenDtos);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving screens: {ex.Message}");
			}
		}

		[HttpGet("getScreensBySchedule/")]
		public async Task<IActionResult> GetScreensByMovieSchedule(string city, long movieId, DateTime date)
		{
			try
			{
				var screens = await _screenRepository.GetScreensByMovieScheduleAsync(city, movieId, date);
				var screenDtos = new List<ViewAllScreensDto>();
				foreach (var screen in screens)
				{
					var screenDto = _mapper.Map<ViewAllScreensDto>(screen);
					screenDtos.Add(screenDto);
				}
				return Ok(screenDtos);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving screens: {ex.Message}");
			}
		}

		[HttpGet("getScreensByDate/")]
		public async Task<IActionResult> GetScreensByDate(DateTime date)
		{
			try
			{
				var screens = await _screenRepository.GetScreensByDateAsync(date);
				var screenDtos = new List<ViewAllScreensDto>();
				foreach (var screen in screens)
				{
					var screenDto = _mapper.Map<ViewAllScreensDto>(screen);
					screenDtos.Add(screenDto);
				}
				return Ok(screenDtos);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving screens: {ex.Message}");
			}
		}


	}
}
