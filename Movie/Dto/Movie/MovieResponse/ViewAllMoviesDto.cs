using Movie.Models;
using System.ComponentModel.DataAnnotations;

namespace Movie.Dto.Movie.MovieResponse
{
	public class ViewAllMoviesDto
	{
		public long Id { get; set; }
		public string Title { get; set; }

		public string PortraitImgUrl { get; set; }
		public string LandscapeImgUrl { get; set; }

		public double Rating { get; set; }

		public string Genre { get; set; }

	}
}
