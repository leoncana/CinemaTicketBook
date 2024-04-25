using Movie.Models;
using System.ComponentModel.DataAnnotations;

namespace Movie.Dto.Response
{
	public class ViewAllMoviesDto
	{
		public long Id { get; set; }
		public string Title { get; set; }

		public string PortraitImgUrl { get; set; }

		public double Rating { get; set; }

		public List<string> GenreNames { get; set; }

	}
}
