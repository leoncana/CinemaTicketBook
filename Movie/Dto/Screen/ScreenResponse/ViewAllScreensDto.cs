using Movie.Dto.MovieSchedule.MovieScheduleResponse;
using Movie.Models;

namespace Movie.Dto.Screen.ScreenResponse
{
	public class ViewAllScreensDto
	{
		public ViewAllScreensDto()
		{
			Seats = new List<Seat>();
		}
		public long Id { get; set; }

		public string Name { get; set; }

		public string Location { get; set; }

		public ICollection<Seat> Seats { get; set; }

		public string City { get; set; }

		public string ScreenType { get; set; }

		public ICollection<MovieScheduleDto> MovieSchedules { get; set; }
	}
}
