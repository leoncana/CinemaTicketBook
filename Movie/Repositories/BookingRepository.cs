using Microsoft.EntityFrameworkCore;
using Movie.Data;
using Movie.Interfaces;
using Movie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
				var user = await _context.Users.FindAsync(userId);
				if (user == null)
				{
					return false;
				}

				var seats = await _context.Seats.Where(s => seatIds.Contains(s.SeatId)).ToListAsync();
				if (seats == null || seats.Count != seatIds.Count)
				{
					return false;
				}

				var movieSchedule = await _context.MovieSchedules.Include(ms => ms.Screen).FirstOrDefaultAsync(ms =>
					ms.MovieId == booking.MovieId &&
					ms.ScreenId == booking.ScreenId &&
					ms.ShowTime == booking.ShowTime &&
					ms.ShowDate == booking.ShowDate);

				if (movieSchedule == null)
				{
					return false;
				}

				var unavailableSeats = movieSchedule.NotAvailableSeats.Select(s => s.SeatId);
				booking.Seats = new List<Seat>();
				foreach (var seat in seats)
				{
					if (unavailableSeats.Contains(seat.SeatId) || !movieSchedule.Screen.Seats.Any(s => s.SeatId == seat.SeatId))
					{
						throw new Exception("Seats are booked or do not exist!");
					}

					booking.TotalPrice += seat.Price;
					booking.Seats.Add(seat);
					movieSchedule.NotAvailableSeats.Add(seat);
				}

				booking.UserId = userId;
				_context.Bookings.Add(booking);

				var payment = new Payment
				{
					UserId = booking.UserId,
					PaymentType = booking.PaymentType,
				};

				booking.Payment = payment;
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
				.Include(s => s.Payment)
				.ToListAsync();
		}

		public async Task<Booking> GetBookingByIdAsync(long id)
		{
			return await _context.Bookings
				.Include(b => b.User)
				.Include(b => b.Movie)
				.Include(b => b.Screen)
				.Include(b => b.Seats)
				.Include(b => b.Payment)
				.FirstOrDefaultAsync(s => s.Id == id);
		}

		public async Task<bool> EditBookingAsync(Booking booking)
		{
			_context.Bookings.Update(booking);
			return await SaveAsync();
		}

		public async Task<bool> DeleteBookingAsync(Booking booking)
		{
			var movieSchedule = await _context.MovieSchedules.FirstOrDefaultAsync(ms =>
				ms.MovieId == booking.MovieId &&
				ms.ScreenId == booking.ScreenId &&
				ms.ShowTime == booking.ShowTime &&
				ms.ShowDate == booking.ShowDate);

			if (movieSchedule == null)
			{
				return false;
			}

			_context.Bookings.Remove(booking);

			foreach (var seat in booking.Seats)
			{
				movieSchedule.NotAvailableSeats.Remove(seat);
			}

			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<ICollection<Booking>> GetBookingsByUserIdAsync(long userId)
		{
			return await _context.Bookings
				.Include(b => b.User)
				.Include(b => b.Movie)
				.Include(b => b.Screen)
				.Include(b => b.Seats)
				.Include(b => b.Payment)
				.Where(b => b.UserId == userId)
				.ToListAsync();
		}

		public async Task<bool> BookingExistsAsync(Booking booking)
		{
			return await _context.Bookings.AnyAsync(b => b.ShowTime == booking.ShowTime &&
				b.ShowDate == booking.ShowDate &&
				b.MovieId == booking.MovieId &&
				b.ScreenId == booking.ScreenId);
		}

		public async Task<bool> SaveAsync()
		{
			try
			{
				var saved = await _context.SaveChangesAsync();
				return saved > 0;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
