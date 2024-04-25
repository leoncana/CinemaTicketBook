using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movie.Dto.Request;
using Movie.Interfaces;
using Movie.Models;

namespace Movie.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class BookingsController : ControllerBase
	{
		private readonly IBookingRepository _bookingRepository;
		private readonly IMapper _mapper;

		public BookingsController(IBookingRepository bookingRepository, IMapper mapper)
		{
			_bookingRepository = bookingRepository;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> AddBooking([FromBody] BookingCreateDto bookingDto)
		{
			try
			{
				var mappedBooking = _mapper.Map<Booking>(bookingDto);
				var success = await _bookingRepository.AddBookingAsync(mappedBooking, bookingDto.UserId, bookingDto.SeatIds);
				if (success)
					return Ok(new { message = "Booking added successfully" });
				else
					return BadRequest(new { message = "Failed to add booking" });
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetBookings()
		{
			try
			{
				var bookings = await _bookingRepository.GetBookingsAsync();
				return Ok(bookings);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBookingById(long id)
		{
			try
			{
				var booking = await _bookingRepository.GetBookingByIdAsync(id);
				if (booking == null)
					return NotFound(new { message = "Booking not found" });
				else
					return Ok(booking);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> EditBooking(long id, [FromBody] BookingEditDto bookingDto)
		{
			try
			{
				if (bookingDto == null || id != bookingDto.Id)
				{
					return BadRequest("Invalid booking object");
				}

				var mappedBooking = _mapper.Map<Booking>(bookingDto);

				var success = await _bookingRepository.EditBookingAsync(mappedBooking);
				if (success)
					return Ok(new { message = "Booking updated successfully" });
				else
					return BadRequest(new { message = "Failed to update booking" });
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBooking(long id)
		{
			try
			{
				var booking = await _bookingRepository.GetBookingByIdAsync(id);
				if (booking == null)
					return NotFound(new { message = "Booking not found" });

				var success = await _bookingRepository.DeleteBookingAsync(booking);
				if (success)
					return Ok(new { message = "Booking deleted successfully" });
				else
					return BadRequest(new { message = "Failed to delete booking" });
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
			}
		}

		[HttpGet("search")]
		public async Task<IActionResult> SearchBookings(string searchCharacter)
		{
			try
			{
				var bookings = await _bookingRepository.SearchBookingsAsync(searchCharacter);
				return Ok(bookings);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
			}
		}
	}
}
