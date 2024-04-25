using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Movie.Models
{
	public class Celeb
	{
		[Key]
		public long Id { get; set; }
		[Required(ErrorMessage = "Celebrity type is required")]
		public string CelebType { get; set; }

		[Required(ErrorMessage = "Celebrity name is required")]
		public string CelebName { get; set; }

		public string CelebRole { get; set; }

		public string CelebImage { get; set; }
		public ICollection<MovieCeleb> MovieCelebs { get; set; }

	}
}
