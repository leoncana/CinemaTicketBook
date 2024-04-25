using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movie.Models
{
	public class User
	{
		[Key]
		public long Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }


		[Required(ErrorMessage = "City is required")]
		public string City { get; set; }

		[DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy hh:mm tt}")]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		[DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy hh:mm tt}")]
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
		public ICollection<Booking> Bookings { get; set; }

	}
}
