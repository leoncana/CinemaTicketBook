using System.ComponentModel.DataAnnotations;

namespace Movie.Dto.MovieSchedule.MovieScheduleRequest
{

	public class MovieScheduleCreateDto
	{
		[Required(ErrorMessage = "Show time is required")]
		[DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
		public DateTime ShowTime { get; set; }

		[Required(ErrorMessage = "Show date is required")]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
		public DateTime ShowDate { get; set; }

		[Required(ErrorMessage = "Movie ID is required")]
		public long MovieId { get; set; }

		[Required(ErrorMessage = "Screen ID is required")]
		public long ScreenId { get; set; }
	}
}
