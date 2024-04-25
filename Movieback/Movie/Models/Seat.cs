using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Movie.Models
{
	public class Seat
	{
		[Key]
		public long SeatId { get; set; }
		[Required(ErrorMessage = "Row is required")]
		public string Row { get; set; }

		[Required(ErrorMessage = "Column is required")]
		public int Col { get; set; }


		[Required(ErrorMessage = "Price is required")]
		public double Price { get; set; }

		[AllowNull]
		[ForeignKey("BookingId")]
		public long BookingId { get; set; }
		public Booking Booking { get; set; }
	}
}
