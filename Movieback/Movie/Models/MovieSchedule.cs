using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Models
{
	public class MovieSchedule
	{
		[Required(ErrorMessage = "Movie ID is required")]
		[Key]
		public long MovieScheduleId { get; set; }

		[Required(ErrorMessage = "Show time is required")]
		public string ShowTime { get; set; }

		public List<Seat> NotAvailableSeats { get; set; }

		[Required(ErrorMessage = "Show date is required")]
		[DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy hh:mm tt}")]
		public DateTime ShowDate { get; set; }
		[ForeignKey("MovieId")]
		public long MovieId { get; set; }
		public Moviee Movie { get; set; }
		[ForeignKey("ScreenId")]
		public long ScreenId { get; set; }
		public Screen Screen { get; set; }
	}
}
