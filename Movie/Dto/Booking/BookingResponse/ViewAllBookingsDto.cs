using Movie.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movie.Dto.Booking.BookingResponse
{
	public class ViewAllBookingsDto
	{
		public long Id { get; set; }

		[DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
		public DateTime ShowTime { get; set; }

		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime ShowDate { get; set; }

		[Required(ErrorMessage = "Movie ID is required")]

		public long MovieId { get; set; }

		public long ScreenId { get; set; }

		public double TotalPrice { get; set; }

		public Payment Payment { get; set; }


		public long UserId { get; set; }

		public List<Seat> Seats { get; set; }
	}
}
