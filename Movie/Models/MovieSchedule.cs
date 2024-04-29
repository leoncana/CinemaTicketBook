using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Movie.Models
{
	public class MovieSchedule
	{
		public MovieSchedule()
		{
			NotAvailableSeats = new List<Seat>();
		}
		[Required(ErrorMessage = "Movie ID is required")]
		[Key]
		public long MovieScheduleId { get; set; }

		[Required(ErrorMessage = "Show date is required")]
		[DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
		public DateTime ShowTime { get; set; }

		public ICollection<Seat> NotAvailableSeats { get; set; }

		[Required(ErrorMessage = "Show date is required")]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime ShowDate { get; set; }

		[ForeignKey("MovieId")]
		public long MovieId { get; set; }
		public Moviee Movie { get; set; }
		[ForeignKey("ScreenId")]
		public long ScreenId { get; set; }
		public Screen Screen { get; set; }
	}
}
