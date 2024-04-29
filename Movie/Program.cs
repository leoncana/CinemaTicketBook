using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Movie.Data;
using Movie.Interfaces;
using Movie.Repositories;
using System.Text;

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
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				var connectionString = configuration.GetConnectionString("DefaultConnection");
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
			});


			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
				{
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = configuration["JwtSettings:Issuer"],
						ValidAudience = configuration["JwtSettings:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true
					};
				});

			builder.Services.AddAuthentication();


			// Add services to the container.
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", builder =>
				{
					builder.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod();
				});
			});

			builder.Services.AddControllers();

			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			builder.Services.AddScoped<IBookingRepository, BookingRepository>();
			builder.Services.AddScoped<IMovieRepository, MovieRepository>();
			builder.Services.AddScoped<IScreenRepository, ScreenRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IMovieScheduleRepository, MovieScheduleRepository>();
			builder.Services.AddScoped<IBookingRepository, BookingRepository>();


			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(option =>
			{
				option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
				option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter a valid token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
				option.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							}
						},
						new string[]{}
					}
				});
			});

			var app = builder.Build();
			using (var scope = app.Services.CreateScope())
			{
				var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

				userRepository.CreateAdminUsers().Wait();
			}

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseCors("AllowAll");
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();

			app.Run();

		}
	}
}