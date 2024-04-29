using Humanizer.Localisation;
using Movie.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Dto.Movie.MovieRequest
{
	public class MovieCreateDto
	{
		[Required(ErrorMessage = "Title is required")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Description is required")]
		public string Description { get; set; }


		[Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
		public double Rating { get; set; }

		[Required(ErrorMessage = "Duration is required")]
		public int Duration { get; set; }

		[Required(ErrorMessage = "Genres are required")]
		public Genres Genre { get; set; }

		[NotMapped]
		public IFormFile PortraitPicture { get; set; }
		[NotMapped]
		public IFormFile LandscapePicture { get; set; }
	}

}
