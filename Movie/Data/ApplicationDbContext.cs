using Microsoft.EntityFrameworkCore;
using Movie.Models;

namespace Movie.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<MovieSchedule> MovieSchedules { get; set; }
		public DbSet<Moviee> Movies { get; set; }
		public DbSet<Screen> Screens { get; set; }
		public DbSet<Seat> Seats { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<MoviePicture> MoviePictures { get; set; }
	}
}
