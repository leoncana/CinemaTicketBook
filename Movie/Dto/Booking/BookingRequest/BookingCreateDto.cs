using Movie.Models;
using System.ComponentModel.DataAnnotations;

namespace Movie.Dto.Booking.BookingRequest
{
	public class BookingCreateDto
	{
		[Required(ErrorMessage = "Movie ID is required")]
		public long MovieId { get; set; }
		[Required(ErrorMessage = "Screen ID is required")]
		public long ScreenId { get; set; }
		[Required(ErrorMessage = "Show time is required")]
		[DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
		public DateTime ShowTime { get; set; }

		[Required(ErrorMessage = "Show date is required")]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime ShowDate { get; set; }
		public List<long> SeatIds { get; set; }
		public string PaymentType { get; set; }
	}
}
