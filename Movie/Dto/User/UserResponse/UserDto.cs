
using Movie.Dto.Booking.BookingRequest;
using Movie.Dto.Booking.BookingResponse;

namespace Movie.Dto.User.UserResponse
{
	public class UserDto
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public string City { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public string Role { get; set; }
		public ICollection<ViewAllBookingsDto> Bookings { get; set; }

	}
}
