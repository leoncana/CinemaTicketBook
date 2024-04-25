using System.ComponentModel.DataAnnotations;

namespace Movie.Models
{
	public class Screen
	{
		[Key]
		public long Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Location is required")]
		public string Location { get; set; }

		public List<Seat> Seats { get; set; }

		[Required(ErrorMessage = "City is required")]
		public string City { get; set; }

		[Required(ErrorMessage = "Screen type is required")]
		public string ScreenType { get; set; }

		public ICollection<MovieSchedule> MovieSchedules { get; set; }
	}
}
