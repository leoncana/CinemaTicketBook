using System.ComponentModel.DataAnnotations;

namespace Movie.Models
{
	public enum Cities
	{
		Ferizaj,
		Prizren,
		Gjakove,
		Prishtine,
		Skenderaj,
		Mitrovice,
		Gjilan,
		Peje
	}

	public enum ScreenType
	{
		_2D,
		_3D,
		_4D,
		IMAX
	}
	public class Screen
	{


		public Screen()
		{
			Seats = new List<Seat>();
		}
		[Key]
		public long Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Location is required")]
		public string Location { get; set; }

		public ICollection<Seat> Seats { get; set; }

		[Required(ErrorMessage = "City is required")]
		public Cities City { get; set; }

		[Required(ErrorMessage = "Screen type is required")]
		public ScreenType ScreenType { get; set; }

		public ICollection<MovieSchedule> MovieSchedules { get; set; }
	}
}
