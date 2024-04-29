using AutoMapper;
using Movie.Dto.Booking.BookingRequest;
using Movie.Dto.Booking.BookingResponse;
using Movie.Dto.Movie.MovieRequest;
using Movie.Dto.Movie.MovieResponse;
using Movie.Dto.MovieSchedule.MovieScheduleRequest;
using Movie.Dto.MovieSchedule.MovieScheduleResponse;
using Movie.Dto.Screen.ScreenRequest;
using Movie.Dto.Screen.ScreenResponse;
using Movie.Dto.User.UserRequest;
using Movie.Dto.User.UserResponse;
using Movie.Models;

namespace Movie.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Booking, BookingCreateDto>();
			CreateMap<BookingCreateDto, Booking>();
			CreateMap<MovieCreateDto, Moviee>();
			CreateMap<Moviee, MovieCreateDto>();
			CreateMap<Moviee, MovieEditDto>();
			CreateMap<MovieEditDto, Moviee>();
			CreateMap<Moviee, ViewAllMoviesDto>();
			CreateMap<ViewAllMoviesDto, Moviee>();
			CreateMap<ViewMovieByIdDto, Moviee>();
			CreateMap<Moviee, ViewMovieByIdDto>();
			CreateMap<Screen, ScreenCreateDto>();
			CreateMap<ScreenCreateDto, Screen>();
			CreateMap<MovieScheduleCreateDto, MovieSchedule>();
			CreateMap<MovieSchedule, MovieScheduleCreateDto>();
			CreateMap<Booking, ViewAllBookingsDto>();
			CreateMap<BookingEditDto, Booking>();
			CreateMap<Booking, BookingEditDto>()
			.ForMember(dest => dest.SeatIds, opt => opt.MapFrom(src => src.Seats.Select(s => s.SeatId).ToList()));
			CreateMap<ViewAllBookingsDto, Booking>();
			CreateMap<MovieSchedule, MovieScheduleDto>();
			CreateMap<ViewAllScreensDto, Screen>();
			CreateMap<Screen, ViewAllScreensDto>();
			CreateMap<User, RegisterUserDto>();
			CreateMap<RegisterUserDto, User>();
			CreateMap<User, LoginUserDto>();
			CreateMap<LoginUserDto, User>();
			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>();


		}
	}
}
