//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Movie.Interfaces;
//using Movie.Models;

//namespace Movie.Controllers
//{
//	[ApiController]
//	[Route("[controller]")]
//	public class MovieController : ControllerBase
//	{
//		private readonly IMovieRepository _movieRepository;
//		private readonly IUserRepository _userRepository;
//		private readonly IBookingRepository _bookingRepository;
//		private readonly IScreenRepository _screenRepository;

//		public MovieController(IMovieRepository movieRepository, IUserRepository userRepository, IBookingRepository bookingRepository, IScreenRepository screenRepository)
//		{
//			_movieRepository = movieRepository;
//			_userRepository = userRepository;
//			_bookingRepository = bookingRepository;
//			_screenRepository = screenRepository;
//		}

//		[HttpGet("test")]
//		public IActionResult Test()
//		{
//			return Ok(new { message = "Movie API is working" });
//		}

//		 Admin access
//		[HttpPost("createmovie")]
//		[Authorize]
//		public async Task<IActionResult> CreateMovie(Moviee movie)
//		{
//			try
//			{
//				await _movieRepository.AddMovieAsync(movie);
//				return StatusCode(201, new { ok = true, message = "Movie added successfully" });
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
//			}
//		}

//		[HttpPost("addcelebtomovie")]
//		[Authorize]
//		public async Task<IActionResult> AddCelebToMovie(string movieId, string celebType, string celebName, string celebRole, string celebImage)
//		{
//			try
//			{
//				var movie = await _movieRepository.GetMovieByIdAsync(movieId);
//				if (movie == null)
//				{
//					return NotFound(new { ok = false, message = "Movie not found" });
//				}

//				var newCeleb = new Celeb { CelebType = celebType, CelebName = celebName, CelebRole = celebRole, CelebImage = celebImage };

//				if (celebType == "cast")
//				{
//					movie.Cast.Add(newCeleb);
//				}
//				else
//				{
//					movie.Crew.Add(newCeleb);
//				}

//				await _movieRepository.UpdateMovieAsync(movie);
//				return StatusCode(201, new { ok = true, message = "Celeb added successfully" });
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
//			}
//		}

//		[HttpPost("createscreen")]
//		[Authorize]
//		public async Task<IActionResult> CreateScreen(Screen screen)
//		{
//			try
//			{
//				await _screenRepository.AddScreenAsync(screen);
//				return StatusCode(201, new { ok = true, message = "Screen added successfully" });
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
//			}
//		}

//		[HttpPost("addmoviescheduletoscreen")]
//		[Authorize]
//		public async Task<IActionResult> AddMovieScheduleToScreen(string screenId, string movieId, DateTime showTime, DateTime showDate)
//		{
//			try
//			{
//				var screen = await _screenRepository.GetScreenByIdAsync(screenId);
//				if (screen == null)
//				{
//					return NotFound(new { ok = false, message = "Screen not found" });
//				}

//				var movie = await _movieRepository.GetMovieByIdAsync(movieId);
//				if (movie == null)
//				{
//					return NotFound(new { ok = false, message = "Movie not found" });
//				}

//				screen.MovieSchedules.Add(new MovieSchedule { MovieId = movieId, ShowTime = showTime, NotAvailableSeats = new List<string>(), ShowDate = showDate });
//				await _screenRepository.UpdateScreenAsync(screen);

//				return StatusCode(201, new { ok = true, message = "Movie schedule added successfully" });
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
//			}
//		}

//		 User access
//		[HttpPost("bookticket")]
//		[Authorize]
//		public async Task<IActionResult> BookTicket(string showTime, DateTime showDate, string movieId, string screenId, List<string> seats, decimal totalPrice, string paymentId, string paymentType)
//		{
//			try
//			{
//				 Verify payment id function can be implemented here

//				var screen = await _screenRepository.GetScreenByIdAsync(screenId);
//				if (screen == null)
//				{
//					return NotFound(new { ok = false, message = "Theatre not found" });
//				}

//				var movieSchedule = screen.MovieSchedules.FirstOrDefault(schedule =>
//					schedule.ShowDate.Date == showDate.Date &&
//					schedule.ShowTime == showTime &&
//					schedule.MovieId == movieId);

//				if (movieSchedule == null)
//				{
//					return NotFound(new { ok = false, message = "Movie schedule not found" });
//				}

//				var user = await _userRepository.GetUserByIdAsync(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value);

//				var newBooking = new Booking { UserId = user.Id, ShowTime = showTime, ShowDate = showDate, MovieId = movieId, ScreenId = screenId, Seats = seats, TotalPrice = totalPrice, PaymentId = paymentId, PaymentType = paymentType };
//				await _bookingRepository.AddBookingAsync(newBooking);

//				movieSchedule.NotAvailableSeats.AddRange(seats);
//				await _screenRepository.UpdateScreenAsync(screen);

//				user.Bookings.Add(newBooking.Id);
//				await _userRepository.UpdateUserAsync(user);

//				return StatusCode(201, new { ok = true, message = "Booking successful" });
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
//			}
//		}

//		[HttpGet("movies")]
//		public async Task<IActionResult> GetMovies()
//		{
//			try
//			{
//				var movies = await _movieRepository.GetAllMoviesAsync();
//				return Ok(new { ok = true, data = movies, message = "Movies retrieved successfully" });
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
//			}
//		}

//		[HttpGet("movies/{id}")]
//		public async Task<IActionResult> GetMovieById(string id)
//		{
//			try
//			{
//				var movie = await _movieRepository.GetMovieByIdAsync(id);
//				if (movie == null)
//				{
//					return NotFound(new { ok = false, message = "Movie not found" });
//				}
//				return Ok(new { ok = true, data = movie, message = "Movie retrieved successfully" });
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
//			}
//		}

//		[HttpGet("screensbycity/{city}")]
//		public async Task<IActionResult> GetScreensByCity(string city)
//		{
//			try
//			{
//				city = city.ToLower();
//				var screens = await _screenRepository.GetScreensByCityAsync(city);
//				if (screens == null || !screens.Any())
//				{
//					return NotFound(createResponse(false, "No screens found in the specified city", null));
//				}
//				return Ok(createResponse(true, "Screens retrieved successfully", screens));
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
//			}
//		}

//		[HttpGet("screensbymovieschedule/{city}/{date}/{movieid}")]
//		public async Task<IActionResult> GetScreensByMovieSchedule(string city, DateTime date, string movieId)
//		{
//			try
//			{
//				city = city.ToLower();
//				var screens = await _screenRepository.GetScreensByMovieScheduleAsync(city, date, movieId);
//				return Ok(createResponse(true, "Screens retrieved successfully", screens));
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
//			}
//		}

//		[HttpGet("schedulebymovie/{screenid}/{date}/{movieid}")]
//		public async Task<IActionResult> GetScheduleByMovie(string screenId, DateTime date, string movieId)
//		{
//			try
//			{
//				var screen = await _screenRepository.GetScreenByIdAsync(screenId);
//				if (screen == null)
//				{
//					return NotFound(createResponse(false, "Screen not found", null));
//				}

//				var movieSchedules = screen.MovieSchedules.Where(schedule =>
//					schedule.ShowDate.Date == date.Date &&
//					schedule.MovieId == movieId).ToList();

//				if (movieSchedules == null || !movieSchedules.Any())
//				{
//					return NotFound(createResponse(false, "Movie schedule not found", null));
//				}

//				return Ok(createResponse(true, "Movie schedule retrieved successfully", new { screen, movieSchedulesForDate = movieSchedules }));
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
//			}
//		}

//		[HttpGet("getuserbookings")]
//		[Authorize]
//		public async Task<IActionResult> GetUserBookings()
//		{
//			try
//			{
//				var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
//				var user = await _userRepository.GetUserByIdAsync(userId);
//				if (user == null)
//				{
//					return NotFound(createResponse(false, "User not found", null));
//				}

//				var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId);
//				return Ok(createResponse(true, "User bookings retrieved successfully", bookings));
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
//			}
//		}

//		[HttpGet("getuserbookings/{id}")]
//		[Authorize]
//		public async Task<IActionResult> GetUserBookingById(string id)
//		{
//			try
//			{
//				var booking = await _bookingRepository.GetBookingByIdAsync(id);
//				if (booking == null)
//				{
//					return NotFound(createResponse(false, "Booking not found", null));
//				}

//				return Ok(createResponse(true, "Booking retrieved successfully", booking));
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, new { ok = false, message = "An error occurred", error = ex.Message });
//			}
//		}

//		private object createResponse(bool ok, string message, object data)
//		{
//			return new { ok, message, data };
//		}
//	}
//}
