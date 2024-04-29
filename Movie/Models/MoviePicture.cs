using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Models
{
	public class MoviePicture
	{
		[Key]
		public long ImageId { get; set; }
		[ForeignKey("MovieId")]
		public long MovieId { get; set; }
		[NotMapped]
		public IFormFile Picture { get; set; }

	}
}
