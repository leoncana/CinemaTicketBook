using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Dto.User.UserRequest;
using Movie.Interfaces;
using Movie.Models;

namespace Movie.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{

		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		public UserController(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterUserDto userDto)
		{
			try
			{
				var mappedUser = _mapper.Map<User>(userDto);
				if (await _userRepository.RegisterUser(mappedUser))
				{
					return Ok("Registered!");
				}
				else
				{
					return BadRequest();
				}
			}
			catch (Exception)
			{
				return BadRequest("An error occurred while registering.");
			}
		}


		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginUserDto userDto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest();
				}
				var mappedUser = _mapper.Map<User>(userDto);
				var validatedUser = _userRepository.ValidateCredentials(mappedUser.Email, mappedUser.Password);
				var token = _userRepository.GenerateTokenString(await validatedUser);

				if (token != null)
				{
					return Ok(new { Token = token });
				}
				else
				{
					return BadRequest("Invalid email or password.");
				}
			}
			catch (NullReferenceException)
			{
				return BadRequest("Invalid email or password.");
			}
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> GetUserByUsername(string username)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest();
				}
				var user = await _userRepository.GetUserByUsernameAsync(username);
				if (user != null)
				{
					return Ok(user);
				}
				else
				{
					return BadRequest("User not found.");
				}
			}
			catch (Exception)
			{
				return BadRequest("An error occurred while processing your request.");
			}
		}

	}
}
