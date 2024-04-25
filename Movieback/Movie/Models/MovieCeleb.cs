namespace Movie.Models
{
	public class MovieCeleb
	{
		public Moviee Movie { get; set; }
		public long MovieId { get; set; }

		public Celeb Celeb { get; set; }
		public long CelebId { get; set; }
	}
}
