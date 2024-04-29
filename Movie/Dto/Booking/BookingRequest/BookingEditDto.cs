using System.ComponentModel.DataAnnotations;

namespace Movie.Dto.Booking.BookingRequest
{
	public class BookingEditDto
	{
		public long Id { get; set; }
		public List<long> SeatIds { get; set; }
	}
}

