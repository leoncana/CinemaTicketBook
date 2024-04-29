using Movie.Models;
using System.ComponentModel.DataAnnotations;

namespace Movie.Dto.MovieSchedule.MovieScheduleResponse
{
	public class MovieScheduleDto
	{
		public MovieScheduleDto()
		{
			NotAvailableSeats = new List<Seat>();
		}
		public long MovieScheduleId { get; set; }

		[DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
		public DateTime ShowTime { get; set; }


		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime ShowDate { get; set; }
		public ICollection<Seat> NotAvailableSeats { get; set; }

		public long MovieId { get; set; }
		public long ScreenId { get; set; }
	}
}
