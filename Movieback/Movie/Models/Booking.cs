using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Models
{
	public class Booking
	{
		[Key]
		public long Id { get; set; }

		[Required(ErrorMessage = "Show time is required")]
		public string ShowTime { get; set; }

		[Required(ErrorMessage = "Show date is required")]
		public DateTime ShowDate { get; set; }

		[Required(ErrorMessage = "Movie ID is required")]
		[ForeignKey("MovieId")]

		public long MovieId { get; set; }
		public Moviee Movie { get; set; }


		[Required(ErrorMessage = "Screen ID is required")]
		[ForeignKey("ScreenId")]

		public long ScreenId { get; set; }
		public Screen Screen { get; set; }


		[Required(ErrorMessage = "Total price is required")]
		public double TotalPrice { get; set; }

		[Required(ErrorMessage = "Payment ID is required")]
		public string PaymentId { get; set; }

		[Required(ErrorMessage = "Payment type is required")]
		public string PaymentType { get; set; }
		[Required(ErrorMessage = "UserId is required")]

		[ForeignKey("UserId")]
		public long UserId { get; set; }
		public User User { get; set; }

		[Required(ErrorMessage = "Seats are required")]
		public ICollection<Seat> Seats { get; set; }
	}

}
