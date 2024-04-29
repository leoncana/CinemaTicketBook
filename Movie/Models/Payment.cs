using System.ComponentModel.DataAnnotations;

namespace Movie.Models
{
	public class Payment
	{
		[Key]
		public long Id { get; set; }
		public long UserId { get; set; }
		public string PaymentType { get; set; }

	}
}
