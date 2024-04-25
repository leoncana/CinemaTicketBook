using AutoMapper;
using Movie.Dto.Request;
using Movie.Dto.Response;
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

		}
	}
}
