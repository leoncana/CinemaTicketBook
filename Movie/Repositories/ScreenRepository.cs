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
	public class ScreenRepository : IScreenRepository
	{
		private readonly ApplicationDbContext _context;

		public ScreenRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> AddScreenAsync(Screen screen)
		{
			try
			{
				if (screen == null)
				{
					throw new Exception("Screen object is null");
				}

				if (await ScreenExistsAsync(screen.Name))
				{
					throw new Exception("This screen already exists.");
				}

				if (!Enum.IsDefined(typeof(Cities), screen.City))
				{
					throw new Exception("Invalid city.");
				}

				if (!Enum.IsDefined(typeof(ScreenType), screen.ScreenType))
				{
					throw new Exception("Invalid screen type.");
				}

				// Create seats for the screen
				string[] rows = { "A", "B", "C", "D", "E", "F", "G", "H" };
				string[] types = { "gold", "platinum", "silver" };
				foreach (var row in rows)
				{
					for (int col = 1; col <= 20; col++)
					{
						string seatType = row switch
						{
							"A" or "B" or "C" => "gold",
							"D" or "E" or "F" => "platinum",
							_ => "silver"
						};

						screen.Seats.Add(new Seat
						{
							Row = row,
							Col = col,
							Type = seatType,
							Price = seatType switch
							{
								"gold" => 10.0,
								"platinum" => 7.0,
								"silver" => 5.0,
								_ => 0.0
							}
						});
					}
				}

				_context.Screens.Add(screen);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to add screen: {ex.Message}");
			}
		}


		public async Task<ICollection<Screen>> GetAllScreensAsync()
		{
			try
			{
				return await _context.Screens.Include(s => s.Seats).Include(s => s.MovieSchedules).ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to retrieve screens: {ex.Message}");
			}
		}

		public async Task<Screen> GetScreenByIdAsync(long id)
		{
			try
			{
				return await _context.Screens.Include(s => s.Seats).Include(s => s.MovieSchedules).FirstOrDefaultAsync(s => s.Id == id);
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to retrieve screen: {ex.Message}");
			}
		}

		public async Task<bool> EditScreenAsync(Screen screen)
		{
			try
			{
				_context.Screens.Update(screen);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to edit screen: {ex.Message}");
			}
		}

		public async Task<bool> DeleteScreenAsync(Screen screen)
		{
			try
			{
				_context.Screens.Remove(screen);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to delete screen: {ex.Message}");
			}
		}

		public async Task<ICollection<Screen>> GetScreensByMovieScheduleAsync(string city, long movieId, DateTime date)
		{
			try
			{
				if (!Enum.TryParse(city, true, out Cities cityEnum))
				{
					throw new Exception($"Invalid city: {city}");
				}

				var screens = await _context.Screens
					.Include(s => s.MovieSchedules)
					.Where(s => s.City == cityEnum && s.MovieSchedules.Any(ms => ms.ShowDate.Date == date.Date && ms.MovieId == movieId))
					.ToListAsync();

				if (screens.Count == 0)
				{
					throw new Exception("No screens found in the specified schedule");
				}

				var filteredScreens = new List<Screen>();
				foreach (var screen in screens)
				{
					foreach (var schedule in screen.MovieSchedules)
					{
						if (schedule.ShowDate.Date == date.Date && schedule.MovieId == movieId)
						{
							filteredScreens.Add(screen);
							break;
						}
					}
				}

				return filteredScreens;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to retrieve screens by movie schedule: {ex.Message}");
			}
		}

		public async Task<ICollection<Screen>> GetScreensByCityAsync(string city)
		{
			try
			{
				if (!Enum.TryParse(city, true, out Cities cityEnum))
				{
					throw new Exception($"Invalid city: {city}");
				}

				var screens = await _context.Screens
					.Include(s => s.MovieSchedules)
					.Where(s => s.City == cityEnum)
					.ToListAsync();

				if (screens.Count == 0)
				{
					throw new Exception("No screens found in the specified city");
				}

				return screens;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to retrieve screens by city: {ex.Message}");
			}
		}




		public async Task<ICollection<Screen>> GetScreensByDateAsync(DateTime date)
		{
			try
			{
				var screens = await _context.Screens
					.Include(s => s.MovieSchedules)
					.Where(s => s.MovieSchedules.Any(ms => ms.ShowDate.Date == date.Date))
					.ToListAsync();

				if (screens.Count == 0)
				{
					throw new Exception("No screens found for the specified date");
				}

				return screens;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to retrieve screens by date: {ex.Message}");
			}
		}


		public async Task<bool> ScreenExistsAsync(string name)
		{
			try
			{
				return await _context.Screens.AnyAsync(s => s.Name == name);
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to check screen existence: {ex.Message}");
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
