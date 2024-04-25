using Microsoft.EntityFrameworkCore;
using Movie.Data;
using Movie.Interfaces;
using Movie.Repositories;

namespace Movie
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false)
				.Build();

			// Add services to the container.
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));

			builder.Services.AddControllers();

			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


			builder.Services.AddScoped<IBookingRepository, BookingRepository>();
			builder.Services.AddScoped<IMovieRepository, MovieRepository>();
			builder.Services.AddScoped<IScreenRepository, ScreenRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();

			app.Run();
		}
	}
}