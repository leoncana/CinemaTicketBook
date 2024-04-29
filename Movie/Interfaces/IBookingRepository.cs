using Movie.Models;

namespace Movie.Interfaces
{
	public interface IBookingRepository
	{
		Task<bool> AddBookingAsync(Booking booking, long userId, List<long> seatIds);
		Task<ICollection<Booking>> GetBookingsAsync();
		Task<Booking> GetBookingByIdAsync(long id);
		Task<ICollection<Booking>> GetBookingsByUserIdAsync(long userId);
		Task<bool> EditBookingAsync(Booking booking);
		Task<bool> DeleteBookingAsync(Booking booking);
		Task<bool> BookingExistsAsync(Booking booking);
		Task<bool> SaveAsync();
	}
}
