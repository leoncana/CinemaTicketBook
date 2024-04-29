using Movie.Models;
using System.ComponentModel.DataAnnotations;

namespace Movie.Dto.Screen.ScreenRequest
{
	public class ScreenCreateDto
	{
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Location is required")]
		public string Location { get; set; }
		[Required(ErrorMessage = "City is required")]
		public Cities City { get; set; }
		[Required(ErrorMessage = "Screen type is required")]
		public ScreenType ScreenType { get; set; }
	}
}
