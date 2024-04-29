using Movie.Models;
using System.ComponentModel.DataAnnotations;

namespace Movie.Dto.User.UserRequest
{
	public class RegisterUserDto
	{
		public string Name { get; set; }
		public string Email { get; set; }

		public string Password { get; set; }


		public Cities City { get; set; }

	}
}
