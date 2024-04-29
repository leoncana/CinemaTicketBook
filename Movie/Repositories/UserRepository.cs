using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movie.Data;
using Movie.Dto.User.UserResponse;
using Movie.Interfaces;
using Movie.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Movie.Repositories
{
	public class UserRepository : IUserRepository
	{
		public readonly IConfiguration _configuration;
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UserRepository(IConfiguration configuration, ApplicationDbContext context, IMapper mapper)
		{
			_configuration = configuration;
			_context = context;
			_mapper = mapper;
		}

		public async Task<bool> RegisterUser(User user)
		{
			var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
			if (existingUser != null)
			{
				throw new Exception("User with this email already exists.");
			}
			if (!Enum.IsDefined(typeof(Cities), user.City))
			{
				throw new Exception("Invalid city.");
			}
			var newUser = new User
			{
				Name = user.Name,
				Email = user.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
				City = user.City,
				Role = "User"
			};
			_context.Users.Add(newUser);
			await _context.SaveChangesAsync();
			return true;
		}
		public string GenerateTokenString(User user)
		{
			var claims = new List<Claim>
			{
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role),
			new Claim("userId", user.Id.ToString()),
			};

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value));

			var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

			JwtSecurityToken securityToken = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddMinutes(30),
				issuer: _configuration.GetSection("JwtSettings:Issuer").Value,
				audience: _configuration.GetSection("JwtSettings:Audience").Value,
				signingCredentials: signingCredentials);
			;
			string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
			return token;
		}

		public async Task<UserDto> GetUserByUsernameAsync(string username)
		{
			var user = await _context.Users.Include(u => u.Bookings).FirstOrDefaultAsync(u => u.Email == username);
			var mappedUser = _mapper.Map<UserDto>(user);
			if (user == null)
			{
				throw new Exception("User is null");
			}
			else
			{
				return mappedUser;
			}
		}

		public async Task<string> Login(User user)
		{
			var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
			if (existingUser != null && existingUser.Password == user.Password)
			{
				return GenerateTokenString(existingUser);
			}
			else
			{
				throw new Exception("Invalid email or password.");
			}
		}



		public async Task<User> ValidateCredentials(string username, string password)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == username);
			if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
			{
				return user;
			}
			return null;
		}

		public async Task CreateAdminUsers()
		{
			try
			{
				var existingAdmins = await _context.Users.AnyAsync(u => u.Role == "Admin");
				if (!existingAdmins)
				{
					for (int i = 1; i <= 4; i++)
					{
						var adminUser = new User
						{
							Name = $"Admin{i}",
							Email = $"admin{i}@movieadmin.com",
							Password = BCrypt.Net.BCrypt.HashPassword($"Admin.{i}"),
							City = Cities.Prishtine,
							Role = "Admin"
						};
						_context.Users.Add(adminUser);
					}
					await _context.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

	}
}