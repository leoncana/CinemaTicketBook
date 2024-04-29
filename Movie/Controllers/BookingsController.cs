using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Dto.Booking.BookingRequest;
using Movie.Dto.Booking.BookingResponse;
using Movie.Interfaces;
using Movie.Models;
using System.Security.Claims;

namespace Movie.Controllers
{
	[Authorize]
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
				var userId = long.Parse(User.FindFirst("userId").Value);
				if (userId == null)
				{
					return Unauthorized();
				}
				var mappedBooking = _mapper.Map<Booking>(bookingDto);
				var success = await _bookingRepository.AddBookingAsync(mappedBooking, userId, bookingDto.SeatIds);
				if (!success)
				{
					return BadRequest("Failed to add booking due to invalid user, seats, or movie schedule.");
				}

				var mappedBookingDto = _mapper.Map<ViewAllBookingsDto>(mappedBooking);
				return CreatedAtAction(nameof(GetBookingById), new { id = mappedBookingDto.Id }, mappedBookingDto);
			}
			catch (Exception ex)
			{
				return BadRequest($"Error adding booking: {ex.Message}");
			}
		}


		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> GetBookings()
		{
			try
			{
				var bookings = await _bookingRepository.GetBookingsAsync();
				var bookingDtos = new List<ViewAllBookingsDto>();
				foreach (var booking in bookings)
				{
					var bookingDto = _mapper.Map<ViewAllBookingsDto>(booking);
					bookingDtos.Add(bookingDto);
				}
				return Ok(bookingDtos);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
			}
		}


		[Authorize(Roles = "Admin")]
		[HttpGet("getBookingsByUserId/{id}")]
		public async Task<IActionResult> GetBookingsForUser(long id)
		{
			try
			{
				var bookings = await _bookingRepository.GetBookingsByUserIdAsync(id);
				if (bookings == null || bookings.Count == 0)
				{
					return NotFound($"No bookings found for user with ID {id}");
				}

				var bookingDtos = new List<ViewAllBookingsDto>();
				foreach (var booking in bookings)
				{
					var bookingDto = _mapper.Map<ViewAllBookingsDto>(booking);
					bookingDtos.Add(bookingDto);
				}
				return Ok(bookingDtos);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving bookings: {ex.Message}");
			}
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetBookingById(long id)
		{
			try
			{
				var booking = await _bookingRepository.GetBookingByIdAsync(id);

				var bookingDto = _mapper.Map<ViewAllBookingsDto>(booking);
				if (bookingDto == null)
					return NotFound(new { message = "Booking not found" });
				else
					return Ok(bookingDto);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred: {ex.Message}" });
			}
		}


		[HttpGet("getMyBookings/")]
		public async Task<IActionResult> GetUserBookings()
		{
			try
			{
				var userId = long.Parse(User.FindFirst("userId").Value);
				if (userId == null)
				{
					return Unauthorized();
				}
				var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId);
				if (bookings == null || bookings.Count == 0)
				{
					return NotFound($"No bookings found for user with ID {userId}");
				}

				var bookingDtos = new List<ViewAllBookingsDto>();
				foreach (var booking in bookings)
				{
					var bookingDto = _mapper.Map<ViewAllBookingsDto>(booking);
					bookingDtos.Add(bookingDto);
				}
				return Ok(bookingDtos);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving bookings: {ex.Message}");
			}
		}



		[HttpPut("{id}")]
		public async Task<IActionResult> EditBooking(long id, [FromBody] BookingEditDto bookingDto)
		{
			try
			{
				if (bookingDto == null || bookingDto.Id != id)
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
	}
}
