using Movie.Models;
using System.ComponentModel.DataAnnotations;

namespace Movie.Dto.Movie.MovieRequest
{
	public class MovieEditDto
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "Title is required")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Description is required")]
		public string Description { get; set; }

		public string PortraitImgUrl { get; set; }

		public string LandscapeImgUrl { get; set; }

		[Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
		public double Rating { get; set; }

		[Required(ErrorMessage = "Duration is required")]
		public int Duration { get; set; }

		[Required(ErrorMessage = "Genres are required")]
		public List<Genres> Genres { get; set; }

	}
}
