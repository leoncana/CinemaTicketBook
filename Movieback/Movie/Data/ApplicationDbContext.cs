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
		public DbSet<Admin> Admins { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Celeb> Celebs { get; set; }
		public DbSet<MovieSchedule> MovieSchedules { get; set; }
		public DbSet<Moviee> Movies { get; set; }
		public DbSet<MovieCeleb> MovieCelebs { get; set; }
		public DbSet<Screen> Screens { get; set; }
		public DbSet<Seat> Seats { get; set; }
		public DbSet<User> Users { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<MovieCeleb>()
				.HasKey(mc => new { mc.MovieId, mc.CelebId });

			modelBuilder.Entity<MovieCeleb>()
				.HasOne(mc => mc.Movie)
				.WithMany(m => m.MovieCelebs)
				.HasForeignKey(mc => mc.MovieId);

			modelBuilder.Entity<MovieCeleb>()
				.HasOne(mc => mc.Celeb)
				.WithMany(c => c.MovieCelebs)
				.HasForeignKey(mc => mc.CelebId);
		}
	}
}
