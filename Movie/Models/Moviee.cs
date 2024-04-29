using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Movie.Models
{
	public enum Genres
	{
		Action,
		Comedy,
		Drama,
		Fantasy,
		Horror,
		SciFi,
		Thriller,
		Other
	}
	public class Moviee
	{
		[Key]
		public long Id { get; set; }

		[Required(ErrorMessage = "Title is required")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Description is required")]
		public string Description { get; set; }

		[NotMapped]
		public IFormFile MoviePortraitImageUrl { get; set; }

		public string? PortraitImagePath { get; set; }
		[NotMapped]
		public IFormFile MovieLandscapeImageUrl { get; set; }

		public string? LandscapeImagePath { get; set; }

		[Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
		public double Rating { get; set; }

		[Required(ErrorMessage = "Genres are required")]
		public Genres Genre { get; set; }

		[Required(ErrorMessage = "Duration is required")]
		public int Duration { get; set; }

		public ICollection<MovieSchedule> MovieSchedules { get; set; }

	}
}
