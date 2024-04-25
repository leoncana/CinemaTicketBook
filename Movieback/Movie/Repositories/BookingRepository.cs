using Microsoft.EntityFrameworkCore;
using Movie.Data;
using Movie.Interfaces;
using Movie.Models;

namespace Movie.Repositories
{
	public class BookingRepository : IBookingRepository
	{
		private readonly ApplicationDbContext _context;

		public BookingRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<bool> AddBookingAsync(Booking booking, long userId, List<long> seatIds)
		{
			try
			{
				if (await BookingExistsAsync(booking))
				{
					throw new Exception("A booking for this movie and show time already exists.");
				}

				var user = await _context.Users.FindAsync(userId);
				if (user == null)
				{
					throw new Exception("Invalid user. Please check if the user exists.");
				}

				var seats = await _context.Seats.Where(s => seatIds.Contains(s.SeatId)).ToListAsync();

				var movieSchedule = await _context.MovieSchedules.FirstOrDefaultAsync(ms =>
					ms.MovieId == booking.MovieId &&
					ms.ScreenId == booking.ScreenId &&
					ms.ShowTime == booking.ShowTime &&
					ms.ShowDate == booking.ShowDate);

				if (movieSchedule == null)
				{
					throw new Exception("Invalid movie schedule. Please check the availability.");
				}

				var unavailableSeats = movieSchedule.NotAvailableSeats.Select(s => s.SeatId);

				foreach (var seat in seats)
				{
					if (unavailableSeats.Contains(seat.SeatId))
					{
						throw new Exception($"Seat {seat.SeatId} is already booked.");
					}

					booking.TotalPrice += seat.Price;

					seat.BookingId = booking.Id;
					booking.Seats.Add(seat);

					movieSchedule.NotAvailableSeats.Add(seat);
				}

				booking.UserId = userId;

				_context.Bookings.Add(booking);

				await _context.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to add booking: {ex.Message}");
			}
		}


		public async Task<ICollection<Booking>> GetBookingsAsync()
		{
			return await _context.Bookings
				.Include(s => s.Seats)
				.Include(s => s.Movie)
				.Include(s => s.Screen)
				.Include(s => s.Seats)
				.ToListAsync();
		}


		public async Task<Booking> GetBookingByIdAsync(long id)
		{
			return await _context.Bookings
				.Include(b => b.User)
				.Include(b => b.Movie)
				.Include(b => b.Seats)
				.FirstOrDefaultAsync(s => s.Id == id);
		}

		public async Task<ICollection<Booking>> SearchBookingsAsync(string searchCharacter)
		{
			var bookingsQuery = _context.Bookings
				.Include(b => b.User)
				.Include(b => b.Movie)
				.Include(b => b.Screen);

			List<Booking> bookingsList = await bookingsQuery
				.Where(b =>
					EF.Functions.Like(b.ShowTime, $"%{searchCharacter}%") ||
					EF.Functions.Like(b.ShowDate.ToString(), $"%{searchCharacter}%") ||
					EF.Functions.Like(b.Movie.Title, $"%{searchCharacter}%") ||
					EF.Functions.Like(b.User.Name, $"%{searchCharacter}%") ||
					EF.Functions.Like(b.User.Email, $"%{searchCharacter}%") ||
					EF.Functions.Like(b.Screen.Name, $"%{searchCharacter}%") ||
					b.Seats.Any(s => EF.Functions.Like(s.Row, $"%{searchCharacter}%")) ||
					b.Seats.Any(s => EF.Functions.Like(s.Col.ToString(), $"%{searchCharacter}%"))
				)
				.OrderBy(b => b.ShowTime)
				.ToListAsync();

			return bookingsList;
		}

		public async Task<ICollection<Booking>> GetBookingsByUserIdAsync(long userId)
		{
			return await _context.Bookings
				.Where(b => b.Id == userId)
				.ToListAsync();
		}


		public async Task<bool> EditBookingAsync(Booking booking)
		{
			_context.Bookings.Update(booking);
			return await SaveAsync();
		}

		public async Task<bool> DeleteBookingAsync(Booking booking)
		{
			try
			{
				_context.Bookings.Remove(booking);

				var seats = await _context.Seats.Where(s => s.BookingId == booking.Id).ToListAsync();

				foreach (var seat in seats)
				{
					//TODO: Fix this
					seat.BookingId = -1;
				}

				var movieSchedule = await _context.MovieSchedules.FirstOrDefaultAsync(ms =>
					ms.MovieId == booking.MovieId &&
					ms.ScreenId == booking.ScreenId &&
					ms.ShowTime == booking.ShowTime &&
					ms.ShowDate == booking.ShowDate);

				if (movieSchedule != null)
				{
					movieSchedule.NotAvailableSeats.RemoveAll(s => seats.Any(seat => seat.SeatId == s.SeatId));
				}

				return await SaveAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to delete booking: {ex.Message}");
			}
		}


		public async Task<bool>
			BookingExistsAsync(Booking booking)
		{
			try
			{
				return await _context.Bookings.AnyAsync(b => b.ShowTime == booking.ShowTime &&
					b.ShowDate == booking.ShowDate &&
					b.MovieId == booking.MovieId &&
					b.ScreenId == booking.ScreenId);
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to check booking existence: {ex.Message}");
			}
		}
		public async Task<bool> SaveAsync()
		{
			try
			{
				var saved = await _context.SaveChangesAsync();
				return saved > 0;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to save changes: {ex.Message}");
			}
		}
	}
}
